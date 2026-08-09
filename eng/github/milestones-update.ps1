# 1. Define all 19 milestones across the 4 major architectural phases
$milestones = @(
    # --- PHASE 1: FOUNDATIONS ---
    @{ Title = "Phase 1 - Release 0.1: Engineering Governance"; Desc = "Establish the engineering governance, repository standards, documentation, architectural decision process, contribution model, automation, development tooling, and quality practices that provide a scalable foundation for the entire AIQuantTradingResearch platform." },
    @{ Title = "Phase 1 - Release 0.2: Business & Data Foundation"; Desc = "Define the product vision, business rules, domain model, market data concepts, provider abstractions, data lifecycle, quality standards, storage architecture, and platform vision that establish a consistent foundation for quantitative research." },
    @{ Title = "Phase 1 - Release 0.3: Solution Architecture"; Desc = "Design the platform architecture by defining modules, architectural style, boundaries, dependency rules, solution structure, contracts, and system organization to ensure scalability, modularity, maintainability, and long-term evolution." },
    @{ Title = "Phase 1 - Release 0.4: Solution Design"; Desc = "Specify implementation-independent design decisions including extensibility, plugin architecture, public contracts, configuration, error handling, versioning, module interactions, and engineering principles that guide future implementation." },
    @{ Title = "Phase 1 - Release 0.5: Platform Resilience"; Desc = "Design resilient platform behavior through failure classification, retry policies, timeout strategies, circuit breakers, fault tolerance, graceful degradation, and recovery models that support reliable long-term operation." },
    @{ Title = "Phase 1 - Release 0.6: Implementation Foundation"; Desc = "Define implementation standards including coding principles, project structure, naming conventions, testing, logging, observability, dependency injection, and engineering practices that establish a consistent development framework." },
    
    # --- PHASE 2: PLATFORM BOOTSTRAP ---
    @{ Title = "Phase 2 - Release 0.7: AI Engineering Toolkit"; Desc = "Create a reusable engineering toolkit containing AI playbooks, prompt libraries, automation templates, and implementation guides that standardize AI-assisted software development while ensuring consistency, quality, and repeatability across the platform. Treat prompts as source code." },
    @{ Title = "Phase 2 - Release 0.8: Solution Skeleton"; Desc = "Transform the documented architecture into a working .NET solution by creating the initial project structure, module layout, dependency graph, composition root, configuration model, and executable application skeleton without business functionality." },
    @{ Title = "Phase 2 - Release 0.9: Build, CI & Quality Gates"; Desc = "Establish automated engineering workflows including builds, testing, formatting, static analysis, package management, continuous integration, verification scripts, and quality gates that guarantee consistent engineering standards." },
    @{ Title = "Phase 2 - Release 1.0: Plugin Framework"; Desc = "Implement the platform extensibility model by enabling dynamic plugin discovery, module registration, provider integration, extension points, lifecycle management, and independent capability development without modifying the platform core." },
    
    # --- PHASE 3: CORE PLATFORM ---
    @{ Title = "Phase 3 - Release 1.1: Market Data Platform"; Desc = "Build the market data platform by implementing provider integrations, historical and real-time acquisition, normalization, validation, provider abstraction, metadata management, and reliable data ingestion for quantitative research." },
    @{ Title = "Phase 3 - Release 1.2: Storage"; Desc = "Implement scalable storage capabilities supporting datasets, metadata, catalogs, caching, indexing, persistence, retrieval, and versioning while preserving data integrity, reproducibility, and long-term research consistency." },
    @{ Title = "Phase 3 - Release 1.3: Pipelines"; Desc = "Develop configurable data pipelines that orchestrate ingestion, validation, transformation, enrichment, feature generation, scheduling, monitoring, and execution to create reliable, reproducible workflows for quantitative analysis." },

    # --- PHASE 4: QUANTITATIVE RESEARCH ---
    @{ Title = "Phase 4 - Release 1.4: Feature Engineering"; Desc = "Design and implement reusable feature engineering pipelines that transform validated market data into high-quality, reproducible datasets suitable for quantitative analysis, statistical research, and machine learning experimentation." },
    @{ Title = "Phase 4 - Release 1.5: Research Workspace"; Desc = "Build an integrated research environment for exploratory analysis, experimentation, dataset inspection, visualization, notebooks, and reproducible workflows that accelerate quantitative research while preserving engineering standards." },
    @{ Title = "Phase 4 - Release 1.6: Strategy Framework"; Desc = "Develop a modular strategy framework that enables researchers to design, compose, execute, and compare trading strategies using standardized contracts, reusable components, and configurable execution workflows." },
    @{ Title = "Phase 4 - Release 1.7: Backtesting Engine"; Desc = "Implement a deterministic backtesting engine capable of simulating realistic trading execution, transaction costs, slippage, portfolio accounting, and performance evaluation while ensuring reproducible research results." },
    @{ Title = "Phase 4 - Release 1.8: Portfolio Analytics"; Desc = "Provide portfolio construction and quantitative analytics including optimization, exposure measurement, position sizing, risk-adjusted performance, drawdown analysis, attribution, and comparative strategy evaluation." },
    @{ Title = "Phase 4 - Release 1.9: Machine Learning"; Desc = "Integrate machine learning capabilities supporting dataset preparation, feature selection, model training, validation, experimentation, inference, and reproducible AI workflows for quantitative market research." },
    @{ Title = "Phase 4 - Release 2.0: Explainable AI"; Desc = "Introduce explainable AI techniques that improve model transparency through feature importance, prediction interpretation, diagnostic analysis, and model validation to increase confidence in AI-driven research outcomes." }
)

# 2. Assign absolute verified path to executable
$ghExe = "C:\Program Files\GitHub CLI\gh.exe"

# 3. Create milestones iteratively via API Post operations
foreach ($ms in $milestones) {
    Write-Host " [PROVISIONING] $($ms.Title)..." -ForegroundColor Cyan
    
    # Escape quotes cleanly to prevent JSON structural failures
    $titleEscaped = $ms.Title -replace '"', '\"'
    $descEscaped  = $ms.Desc -replace '"', '\"'
    
    & $ghExe api repos/:owner/:repo/milestones `
       -X POST `
       -f title="$titleEscaped" `
       -f description="$descEscaped" | Out-Null
       
    if ($LASTEXITCODE -eq 0) {
        Write-Host " [SUCCESS] Milestone generated cleanly!" -ForegroundColor Green
    } else {
        Write-Host " [ERROR] Failed to push item tracking object." -ForegroundColor Red
    }
}

Write-Host "✨ Architecture roadmap initialization complete!" -ForegroundColor Green
