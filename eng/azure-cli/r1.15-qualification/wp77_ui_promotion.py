"""Exact WP77 UI image transaction. Default has no live adapter or side effects."""
import json
import re

SUB = "7f5eabb5-80f5-4369-88be-b14a6de763f1"
GROUP = "rg-aiq-r112-wp05-wus2-7f5eabb5"
APP = "aiqr112wp05wus27f5eabb5"
BASE = "/subscriptions/" + SUB + "/resourceGroups/" + GROUP + "/providers/Microsoft.Web/"
SITE = BASE + "sites/" + APP
PLAN = BASE + "serverfarms/asp-aiq-r112-wp05-wus2-7f5eabb5"
REPO = "ghcr.io/samuel-santos-engineer/aiquanttradingresearch@sha256:"
TARGET = "DOCKER|" + REPO + "6b59d2052fdd99d77aae248aa159a0cadba740a91094fbeb0872935a96877dde"
OLD = "DOCKER|" + REPO + "a06baf213628603ab88d0fa0d4fff6977f3ae80c5e2114d174b98763bef6da07"
SECURITY = ("ipSecurityRestrictions", "scmIpSecurityRestrictions", "scmIpSecurityRestrictionsUseMain",
            "minTlsVersion", "scmMinTlsVersion", "ftpsState", "vnetRouteAllEnabled", "webSocketsEnabled")


class Stop(Exception):
    pass


def require(condition, category):
    if not condition: raise Stop(category)


def properties(value, path):
    require(isinstance(value, dict) and str(value.get("id", "")).lower() == path.lower(), "RESPONSE_IDENTITY")
    require(isinstance(value.get("properties"), dict), "RESPONSE_SCHEMA")
    return value["properties"]


def slot_properties(value):
    # Called only for the exact UI /config/slotConfigNames request, after site
    # identity verification. The approved read observed an explicit null ID.
    require(isinstance(value, dict) and "id" in value, "SLOT_RESPONSE_SCHEMA")
    identity = value["id"]
    require(identity is None or isinstance(identity, str) and
            identity.lower() == (SITE + "/config/slotConfigNames").lower(), "RESPONSE_IDENTITY")
    values = value.get("properties")
    fields = {"appSettingNames", "connectionStringNames", "azureStorageConfigNames"}
    require(isinstance(values, dict) and set(values) == fields, "SLOT_SCHEMA")
    require(all(v is None or isinstance(v, list) and all(isinstance(x, str) for x in v)
                for v in values.values()), "SLOT_SCHEMA")
    return values


def execute(request):
    result = {"result": "BLOCKED", "patchAttempts": 0, "UI_IMAGE_CONFIGURED": False,
              "UI_RUNNING_IMAGE_VERIFIED": False, "API_HEALTH_ACCEPTED": False,
              "SAME_KEY_PRESENT": False, "BTC_ETH_BROWSER_ACCEPTED": False,
              "PUBLIC_RUNTIME_VERIFIED": False, "liveAttemptReady": False}
    stage = "UI_SITE_BASELINE"
    private = []
    try:
        site_document = request("GET", SITE)
        site = properties(site_document, SITE)
        require("linux" in site_document.get("kind", "").lower() and
                re.sub("[^a-z0-9]", "", site_document.get("location", "").lower()) == "westus2",
                "SITE_PLATFORM_DRIFT")
        result["siteChecks"] = {
            "expectedPlan": str(site.get("serverFarmId", "")).lower() == PLAN.lower(),
            "running": site.get("state") == "Running", "usageNormal": site.get("usageState") == "Normal",
            "availabilityNormal": site.get("availabilityState") == "Normal", "httpsOnly": site.get("httpsOnly") is True}
        require(all(result["siteChecks"].values()), "SITE_STATE_OR_SECURITY_DRIFT")
        stage = "UI_WEB_BASELINE"
        web = properties(request("GET", SITE + "/config/web"), SITE + "/config/web")
        require(web.get("linuxFxVersion") in (OLD, TARGET) and web.get("appCommandLine") == "", "IMAGE_OR_STARTUP_DRIFT")
        security = {key: web.get(key) for key in SECURITY}
        stage = "PRIVATE_SETTINGS_BASELINE"
        settings = properties(request("POST", SITE + "/config/appsettings/list"), SITE + "/config/appsettings")
        private.append(settings)
        require(all(isinstance(k, str) and (v is None or isinstance(v, str)) for k, v in settings.items()), "SETTINGS_SCHEMA")
        require(settings.get("WEBSITES_PORT") == "8501", "UI_PORT_DRIFT")
        result["uiKeyPresent"] = bool(settings.get("AIQ_RESEARCH_API_KEY"))
        stage = "PRIVATE_SLOT_BASELINE"
        slots = slot_properties(request("GET", SITE + "/config/slotConfigNames"))
        private.append(slots)
        require(all(slots.get(k) is None or isinstance(slots[k], list) and all(isinstance(x, str) for x in slots[k])
                    for k in ("appSettingNames", "connectionStringNames", "azureStorageConfigNames")), "SLOT_SCHEMA")
        result["beforeImage"] = web["linuxFxVersion"][7:]
        result["rollbackBaselineCapturedInMemory"] = True
        if web["linuxFxVersion"] != TARGET:
            stage = "UI_IMAGE_PATCH"
            result["patchAttempts"] = 1
            request("PATCH", SITE + "/config/web", {"properties": {"linuxFxVersion": TARGET, "appCommandLine": ""}})
        stage = "UI_IMAGE_READBACK"
        after_web = properties(request("GET", SITE + "/config/web"), SITE + "/config/web")
        require(after_web.get("linuxFxVersion") == TARGET and after_web.get("appCommandLine") == "", "IMAGE_READBACK_UNVERIFIED")
        result["UI_IMAGE_CONFIGURED"] = True
        result["afterImage"] = TARGET[7:]
        require({key: after_web.get(key) for key in SECURITY} == security, "WEB_SECURITY_PRESERVATION_UNVERIFIED")
        stage = "PRIVATE_SETTINGS_READBACK"
        after_settings = properties(request("POST", SITE + "/config/appsettings/list"), SITE + "/config/appsettings")
        private.append(after_settings)
        require(after_settings == settings, "SETTINGS_PRESERVATION_UNVERIFIED")
        stage = "PRIVATE_SLOT_READBACK"
        after_slots = slot_properties(request("GET", SITE + "/config/slotConfigNames"))
        private.append(after_slots)
        require(after_slots == slots, "SLOT_PRESERVATION_UNVERIFIED")
        stage = "UI_SECURITY_READBACK"
        after_site = properties(request("GET", SITE), SITE)
        require(after_site.get("serverFarmId", "").lower() == PLAN.lower() and after_site.get("httpsOnly") is True,
                "SITE_SECURITY_PRESERVATION_UNVERIFIED")
        result.update(result="IMAGE_DEPLOYMENT_CONFIGURED_INTEGRATION_PENDING", settingsUnchanged=True,
                      slotFlagsUnchanged=True, webSecurityUnchanged=True, httpsAndPlanUnchanged=True)
    except Exception as error:
        result["failedStage"] = stage
        result["failureCategory"] = str(error) if isinstance(error, Stop) else "TRANSPORT_OR_REQUEST_UNVERIFIED"
        result["azureMayHaveChanged"] = result["patchAttempts"] > 0
    finally:
        for values in private: values.clear()
    return result


def main():
    print(json.dumps({"result": "PLAN_ONLY", "target": TARGET[7:], "azureOperations": 0,
                      "PUBLIC_RUNTIME_VERIFIED": False, "liveAttemptReady": False}))
    return 0


if __name__ == "__main__": raise SystemExit(main())
