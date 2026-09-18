az webapp show `
  --resource-group rg-aiq-r112-wp03-wcus-5ec325382770 `
  --name aiqr112wp035ec325382770 `
  --query '{state:state,kind:kind,location:location,httpsOnly:httpsOnly}' `
  --output json