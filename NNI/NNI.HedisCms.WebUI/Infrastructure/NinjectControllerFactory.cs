using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Ninject;
using NNI.HedisCms.Domain.Entities;
using NNI.HedisCms.Domain.Abstract;
using Moq;
using NNI.HedisCms.Domain.Concrete;

namespace NNI.HedisCms.WebUI.Infrastructure
{
    public class NinjectControllerFactory : DefaultControllerFactory
    {
        private IKernel ninjectKernel;

        public NinjectControllerFactory()
        {
            ninjectKernel = new StandardKernel();
            AddBindings();
        }
        protected override IController GetControllerInstance(RequestContext requestContext, Type controllerType)
        {
            return controllerType == null ? null : (IController)ninjectKernel.Get(controllerType);            
        }
        private void AddBindings()
        {
            // Uncomment to use Mock Data
            // MockData();
            // put additional bindings here
            ninjectKernel.Bind<IResourceRepository>().To<EFResourceRepository>();
            ninjectKernel.Bind<IPlanRepository>().To<EFPlanRepository>();
            ninjectKernel.Bind<IActivityLogRepository>().To<EFActivityLogRepository>();
        }
        private void MockData()
        {
            #region MockPlanRepository
            // Mock implementation of the IPlanRepository
            Mock<IPlanRepository> mockPlan = new Mock<IPlanRepository>();
            mockPlan.Setup(m => m.Plans).Returns(new List<Plan>{
                new Plan{
                    // Properties
                    PlanId = 1,
                    PlanName = "BCBS",
                    State = "New York",
                    PlanType = "PPO",
                    DiseaseState = "Diabetes",
                    Grade = "Commendable",

                    // Audit
                    CreatedBy = "Tyson Davila",
                    CreatedDate = DateTime.Now.AddDays(-1),
                    CreatedById = new Guid(),
                    CreatedUtcDate = DateTime.UtcNow,
                    ModifiedBy = "Bob Villa",
                    ModifiedDate = DateTime.Now,
                    ModifiedById = new Guid(),
                    ModifiedUtcDate = DateTime.UtcNow,
                    IsDeleted = false
                },
                new Plan{
                    // Properties
                    PlanId = 2,
                    PlanName = "Cigna",
                    State = "New York",
                    PlanType = "PPO",
                    DiseaseState = "Diabetes",
                    Grade = "Excellent",

                    // Audit
                    CreatedBy = "Tyson Davila",
                    CreatedDate = DateTime.Now.AddDays(-1),
                    CreatedById = new Guid(),
                    CreatedUtcDate = DateTime.UtcNow,
                    ModifiedBy = "Bob Villa",
                    ModifiedDate = DateTime.Now,
                    ModifiedById = new Guid(),
                    ModifiedUtcDate = DateTime.UtcNow,
                    IsDeleted = false
                },
                new Plan{
                    // Properties
                    PlanId = 3,
                    PlanName = "Aetna",
                    State = "New York",
                    PlanType = "PPO",
                    DiseaseState = "Diabetes",
                    Grade = "Poor",

                    // Audit
                    CreatedBy = "Tyson Davila",
                    CreatedDate = DateTime.Now.AddDays(-1),
                    CreatedById = new Guid(),
                    CreatedUtcDate = DateTime.UtcNow,
                    ModifiedBy = "Bob Villa",
                    ModifiedDate = DateTime.Now,
                    ModifiedById = new Guid(),
                    ModifiedUtcDate = DateTime.UtcNow,
                    IsDeleted = false
                }
            }.AsQueryable());
            ninjectKernel.Bind<IPlanRepository>().ToConstant(mockPlan.Object);
            #endregion

            #region MockResourceRepository
            // Mock implementation of the IResourceRepository
            Mock<IResourceRepository> mock = new Mock<IResourceRepository>();
            mock.Setup(m => m.Resources).Returns(new List<Resource>
            {
                // MEDICARE
                new Resource
                { 
                    // Properties
                    ResourceId = 1,
                    Title = "Your diabetes and Medicare Part D: member FAQ",
                    Description = "Answers to your members questions about Medicare Part D coverage",
                    Category = "Medicare",
                    IsPublished = true,
                    ThumbnailName = "thmbMedicareFAQ",
                    ThumbnailExt = ".jpg",
                    ThumbnailMimeType = "image/jpeg",
                    ThumbnailUrl = "~/resources/pdf/thmb/thmbMedicareFAQ.jpg",
                    ResourceName = "MedicareFAQ",
                    ResourceExt = ".pdf",
                    ResourceMimeType = "application/pdf",
                    ResourceUrl = "~/resources/pdf/MedicareFAQ.pdf",
                    ListOrder = 0,
                    // Audit
                    CreatedBy = "Tyson Davila",
                    CreatedDate = DateTime.Now.AddDays(-1),
                    CreatedById = new Guid(),
                    CreatedUtcDate = DateTime.UtcNow,
                    ModifiedBy = "Bob Villa",
                    ModifiedDate = DateTime.Now,
                    ModifiedById = new Guid(),
                    ModifiedUtcDate = DateTime.UtcNow 
                },
                new Resource
                { 
                    // Properties
                    ResourceId = 2,
                    Title = "Low-income subsidy: savings for your Medicare Part D patients with diabetes",
                    Description = "A fact sheet on low-income subsidy for your providers that includes tests for identifying eligible members",
                    Category = "Medicare",
                    IsPublished = true,
                    ThumbnailName = "thmbLowIncomeSubsidy",
                    ThumbnailExt = ".jpg",
                    ThumbnailMimeType = "image/jpeg",
                    ThumbnailUrl = "~/resources/pdf/thmb/thmbLowIncomeSubsidy.jpg",
                    ResourceName = "LowIncomeSubsidy",
                    ResourceExt = ".pdf",
                    ResourceMimeType = "application/pdf",
                    ResourceUrl = "~/resources/pdf/LowIncomeSubsidy.pdf",
                    ListOrder = 1,
                    // Audit
                    CreatedBy = "Tyson Davila",
                    CreatedDate = DateTime.Now.AddDays(-1),
                    CreatedById = new Guid(),
                    CreatedUtcDate = DateTime.UtcNow,
                    ModifiedBy = "Bob Villa",
                    ModifiedDate = DateTime.Now,
                    ModifiedById = new Guid(),
                    ModifiedUtcDate = DateTime.UtcNow 
                },
                // DIABETES MANAGEMENT
                new Resource
                { 
                    // Properties
                    ResourceId = 3,
                    Title = "Glycemic Management Solutions: overview",
                    Description = "A diabetes management initiative for hospitals and other institutions",
                    Category = "Diabetes Management",
                    IsPublished = true,
                    ThumbnailName = "thmbGlycemicOverview",
                    ThumbnailExt = ".jpg",
                    ThumbnailMimeType = "image/jpeg",
                    ThumbnailUrl = "~/resources/pdf/thmb/thmbGlycemicOverview.jpg",
                    ResourceName = "GlycemicOverview",
                    ResourceExt = ".pdf",
                    ResourceMimeType = "application/pdf",
                    ResourceUrl = "~/resources/pdf/GlycemicOverview.pdf",
                    ListOrder = 0,
                    // Audit
                    CreatedBy = "Tyson Davila",
                    CreatedDate = DateTime.Now.AddDays(-1),
                    CreatedById = new Guid(),
                    CreatedUtcDate = DateTime.UtcNow,
                    ModifiedBy = "Bob Villa",
                    ModifiedDate = DateTime.Now,
                    ModifiedById = new Guid(),
                    ModifiedUtcDate = DateTime.UtcNow 
                },
                new Resource
                { 
                    // Properties
                    ResourceId = 4,
                    Title = "Glycemic Management Solutions: institutional assessment",
                    Description = "An assessment of diabetes management practices at hospitals and other institutions",
                    Category = "Diabetes Management",
                    IsPublished = true,
                    ThumbnailName = "thmbGlycemicInstitutionalAssessment",
                    ThumbnailExt = ".jpg",
                    ThumbnailMimeType = "image/jpeg",
                    ThumbnailUrl = "~/resources/pdf/thmb/thmbGlycemicInstitutionalAssessment.jpg",
                    ResourceName = "GlycemicInstitutionalAssessment",
                    ResourceExt = ".pdf",
                    ResourceMimeType = "application/pdf",
                    ResourceUrl = "~/resources/pdf/GlycemicInstitutionalAssessment.pdf",
                    ListOrder = 1,
                    // Audit
                    CreatedBy = "Tyson Davila",
                    CreatedDate = DateTime.Now.AddDays(-1),
                    CreatedById = new Guid(),
                    CreatedUtcDate = DateTime.UtcNow,
                    ModifiedBy = "Bob Villa",
                    ModifiedDate = DateTime.Now,
                    ModifiedById = new Guid(),
                    ModifiedUtcDate = DateTime.UtcNow 
                },
                new Resource
                { 
                    // Properties
                    ResourceId = 5,
                    Title = "Hypoglycemia: a barrier to glycemic control",
                    Description = "A quick look at the impact of gycemia on diabetes management",
                    Category = "Diabetes Management",
                    IsPublished = true,
                    ThumbnailName = "thmbHypoglycemiaControl",
                    ThumbnailExt = ".jpg",
                    ThumbnailMimeType = "image/jpeg",
                    ThumbnailUrl = "~/resources/pdf/thmb/thmbHypoglycemiaControl.jpg",
                    ResourceName = "HypoglycemiaControl",
                    ResourceExt = ".pdf",
                    ResourceMimeType = "application/pdf",
                    ResourceUrl = "~/resources/pdf/HypoglycemiaControl.pdf",
                    ListOrder = 2,
                    // Audit
                    CreatedBy = "Tyson Davila",
                    CreatedDate = DateTime.Now.AddDays(-1),
                    CreatedById = new Guid(),
                    CreatedUtcDate = DateTime.UtcNow,
                    ModifiedBy = "Bob Villa",
                    ModifiedDate = DateTime.Now,
                    ModifiedById = new Guid(),
                    ModifiedUtcDate = DateTime.UtcNow 
                },
                new Resource
                { 
                    // Properties
                    ResourceId = 6,
                    Title = "The facts about hypoglycemia: a case manager's guide",
                    Description = "A resource for case managers that provides tips for recognizing and managing hypoglycemia",
                    Category = "Diabetes Management",
                    IsPublished = true,
                    ThumbnailName = "thmbHypoglycemiaGuide",
                    ThumbnailExt = ".jpg",
                    ThumbnailMimeType = "image/jpeg",
                    ThumbnailUrl = "~/resources/pdf/thmb/thmbHypoglycemiaGuide.jpg",
                    ResourceName = "HypoglycemiaGuide",
                    ResourceExt = ".pdf",
                    ResourceMimeType = "application/pdf",
                    ResourceUrl = "~/resources/pdf/HypoglycemiaGuide.pdf",
                    ListOrder = 3,
                    // Audit
                    CreatedBy = "Tyson Davila",
                    CreatedDate = DateTime.Now.AddDays(-1),
                    CreatedById = new Guid(),
                    CreatedUtcDate = DateTime.UtcNow,
                    ModifiedBy = "Bob Villa",
                    ModifiedDate = DateTime.Now,
                    ModifiedById = new Guid(),
                    ModifiedUtcDate = DateTime.UtcNow 
                },
                new Resource
                { 
                    // Properties
                    ResourceId = 7,
                    Title = "Hypoglycemia signs and symptoms",
                    Description = "A tool to help your members recognize and manage hypoglycemic events",
                    Category = "Diabetes Management",
                    IsPublished = true,
                    ThumbnailName = "thmbHypoglycemiaSigns",
                    ThumbnailExt = ".jpg",
                    ThumbnailMimeType = "image/jpeg",
                    ThumbnailUrl = "~/resources/pdf/thmb/thmbHypoglycemiaSigns.jpg",
                    ResourceName = "HypoglycemiaSigns",
                    ResourceExt = ".pdf",
                    ResourceMimeType = "application/pdf",
                    ResourceUrl = "~/resources/pdf/HypoglycemiaSigns.pdf",
                    ListOrder = 4,
                    // Audit
                    CreatedBy = "Tyson Davila",
                    CreatedDate = DateTime.Now.AddDays(-1),
                    CreatedById = new Guid(),
                    CreatedUtcDate = DateTime.UtcNow,
                    ModifiedBy = "Bob Villa",
                    ModifiedDate = DateTime.Now,
                    ModifiedById = new Guid(),
                    ModifiedUtcDate = DateTime.UtcNow 
                },
                new Resource
                { 
                    // Properties
                    ResourceId = 8,
                    Title = "Type 2 diabetes: an analysis of data from electronic medical records",
                    Description = "An analysis of glycemic control, body mass, and blood pressure in members with type 2 diabetes",
                    Category = "Diabetes Management",
                    IsPublished = true,
                    ThumbnailName = "thmbType2Diabetes",
                    ThumbnailExt = ".jpg",
                    ThumbnailMimeType = "image/jpeg",
                    ThumbnailUrl = "~/resources/pdf/thmb/thmbType2Diabetes.jpg",
                    ResourceName = "Type2Diabetes",
                    ResourceExt = ".pdf",
                    ResourceMimeType = "application/pdf",
                    ResourceUrl = "~/resources/pdf/Type2Diabetes.pdf",
                    ListOrder = 5,
                    // Audit
                    CreatedBy = "Tyson Davila",
                    CreatedDate = DateTime.Now.AddDays(-1),
                    CreatedById = new Guid(),
                    CreatedUtcDate = DateTime.UtcNow,
                    ModifiedBy = "Bob Villa",
                    ModifiedDate = DateTime.Now,
                    ModifiedById = new Guid(),
                    ModifiedUtcDate = DateTime.UtcNow 
                },
                new Resource
                { 
                    // Properties
                    ResourceId = 9,
                    Title = "Living with diabetes: a veteran's guide to diabetes management",
                    Description = "A simple brochure that educates your Veterans Affairs members on disease management",
                    Category = "Diabetes Management",
                    IsPublished = true,
                    ThumbnailName = "thmbVeteransGuide",
                    ThumbnailExt = ".jpg",
                    ThumbnailMimeType = "image/jpeg",
                    ThumbnailUrl = "~/resources/pdf/thmb/thmbVeteransGuide.jpg",
                    ResourceName = "VeteransGuide",
                    ResourceExt = ".pdf",
                    ResourceMimeType = "application/pdf",
                    ResourceUrl = "~/resources/pdf/VeteransGuide.pdf",
                    ListOrder = 6,
                    // Audit
                    CreatedBy = "Tyson Davila",
                    CreatedDate = DateTime.Now.AddDays(-1),
                    CreatedById = new Guid(),
                    CreatedUtcDate = DateTime.UtcNow,
                    ModifiedBy = "Bob Villa",
                    ModifiedDate = DateTime.Now,
                    ModifiedById = new Guid(),
                    ModifiedUtcDate = DateTime.UtcNow 
                },
                new Resource
                { 
                    // Properties
                    ResourceId = 10,
                    Title = "A nurse's guide to managing diabetes in the Veterans Affairs system",
                    Description = "Information on diabetes management for Veterans Affairs nurses",
                    Category = "Diabetes Management",
                    IsPublished = true,
                    ThumbnailName = "thmbNursesGuide ",
                    ThumbnailExt = ".jpg",
                    ThumbnailMimeType = "image/jpeg",
                    ThumbnailUrl = "~/resources/pdf/thmb/thmbNursesGuide.jpg",
                    ResourceName = "NursesGuide",
                    ResourceExt = ".pdf",
                    ResourceMimeType = "application/pdf",
                    ResourceUrl = "~/resources/pdf/NursesGuide.pdf",
                    ListOrder = 7,
                    // Audit
                    CreatedBy = "Tyson Davila",
                    CreatedDate = DateTime.Now.AddDays(-1),
                    CreatedById = new Guid(),
                    CreatedUtcDate = DateTime.UtcNow,
                    ModifiedBy = "Bob Villa",
                    ModifiedDate = DateTime.Now,
                    ModifiedById = new Guid(),
                    ModifiedUtcDate = DateTime.UtcNow 
                },
                new Resource
                { 
                    // Properties
                    ResourceId = 11,
                    Title = "Novo Nordisk - your partner in diabetes care",
                    Description = "A look at Novo Nordisk's history of diabetes research and treatment",
                    Category = "Diabetes Management",
                    IsPublished = true,
                    ThumbnailName = "thmbNNDiabetesPartner",
                    ThumbnailExt = ".jpg",
                    ThumbnailMimeType = "image/jpeg",
                    ThumbnailUrl = "~/resources/pdf/thmb/thmbNNDiabetesPartner.jpg",
                    ResourceName = "NNDiabetesPartner",
                    ResourceExt = ".mp4",
                    ResourceMimeType = "video/mp4",
                    ResourceUrl = "~/resources/pdf/NNDiabetesPartner.mp4",
                    ListOrder = 8,
                    // Audit
                    CreatedBy = "Tyson Davila",
                    CreatedDate = DateTime.Now.AddDays(-1),
                    CreatedById = new Guid(),
                    CreatedUtcDate = DateTime.UtcNow,
                    ModifiedBy = "Bob Villa",
                    ModifiedDate = DateTime.Now,
                    ModifiedById = new Guid(),
                    ModifiedUtcDate = DateTime.UtcNow 
                },
                // QUALITY MEASURES
                new Resource
                { 
                    // Properties
                    ResourceId = 12,
                    Title = "Quality care: standards to enhance healthcare value in diabetes",
                    Description = "Diabetes-related quality measures and guideline recommendations",
                    Category = "Quality Measures",
                    IsPublished = true,
                    ThumbnailName = "thmbQualityGuidelines ",
                    ThumbnailExt = ".jpg",
                    ThumbnailMimeType = "image/jpeg",
                    ThumbnailUrl = "~/resources/pdf/thmb/thmbQualityGuidelines.jpg",
                    ResourceName = "QualityGuidelines",
                    ResourceExt = ".pdf",
                    ResourceMimeType = "application/pdf",
                    ResourceUrl = "~/resources/pdf/QualityGuidelines.pdf",
                    ListOrder = 0,
                    // Audit
                    CreatedBy = "Tyson Davila",
                    CreatedDate = DateTime.Now.AddDays(-1),
                    CreatedById = new Guid(),
                    CreatedUtcDate = DateTime.UtcNow,
                    ModifiedBy = "Bob Villa",
                    ModifiedDate = DateTime.Now,
                    ModifiedById = new Guid(),
                    ModifiedUtcDate = DateTime.UtcNow 
                },
                new Resource
                { 
                    // Properties
                    ResourceId = 13,
                    Title = "Glycemic Management Solutions: resources for provider network population",
                    Description = "A slide presentation that provides an overview of diabetes-related quality measuress and resources for your providers and members",
                    Category = "Quality Measures",
                    IsPublished = true,
                    ThumbnailName = "thmbResourcesForPopulation",
                    ThumbnailExt = ".jpg",
                    ThumbnailMimeType = "image/jpeg",
                    ThumbnailUrl = "~/resources/pdf/thmb/thmbResourcesForPopulation.jpg",
                    ResourceName = "ResourcesForPopulation",
                    ResourceExt = ".pdf",
                    ResourceMimeType = "application/pdf",
                    ResourceUrl = "~/resources/pdf/ResourcesForPopulation.pdf",
                    ListOrder = 1,
                    // Audit
                    CreatedBy = "Tyson Davila",
                    CreatedDate = DateTime.Now.AddDays(-1),
                    CreatedById = new Guid(),
                    CreatedUtcDate = DateTime.UtcNow,
                    ModifiedBy = "Bob Villa",
                    ModifiedDate = DateTime.Now,
                    ModifiedById = new Guid(),
                    ModifiedUtcDate = DateTime.UtcNow 
                },
                // HEALTH ECONOMICS & OUTCOMES RESEARCH
                new Resource
                { 
                    // Properties
                    ResourceId = 14,
                    Title = "About type 2 diabetes and its complications ",
                    Description = "A risk assessment that details potential complications and comorbidities for members with diabetes",
                    Category = "Health Economics &amp; Outcomes Research",
                    IsPublished = true,
                    ThumbnailName = "thmbDiabetesComplications",
                    ThumbnailExt = ".jpg",
                    ThumbnailMimeType = "image/jpeg",
                    ThumbnailUrl = "~/resources/pdf/thmb/thmbDiabetesComplications.jpg",
                    ResourceName = "DiabetesComplications",
                    ResourceExt = ".pdf",
                    ResourceMimeType = "application/pdf",
                    ResourceUrl = "~/resources/pdf/DiabetesComplications.pdf",
                    ListOrder = 0,
                    // Audit
                    CreatedBy = "Tyson Davila",
                    CreatedDate = DateTime.Now.AddDays(-1),
                    CreatedById = new Guid(),
                    CreatedUtcDate = DateTime.UtcNow,
                    ModifiedBy = "Bob Villa",
                    ModifiedDate = DateTime.Now,
                    ModifiedById = new Guid(),
                    ModifiedUtcDate = DateTime.UtcNow 
                },
                new Resource
                { 
                    // Properties
                    ResourceId = 15,
                    Title = "Adherence flashcard",
                    Description = "TK",
                    Category = "Health Economics &amp; Outcomes Research",
                    IsPublished = true,
                    ThumbnailName = "thmbAdherenceFlashcard ",
                    ThumbnailExt = ".jpg",
                    ThumbnailMimeType = "image/jpeg",
                    ThumbnailUrl = "~/resources/pdf/thmb/thmbAdherenceFlashcard.jpg",
                    ResourceName = "AdherenceFlashcard",
                    ResourceExt = ".pdf",
                    ResourceMimeType = "application/pdf",
                    ResourceUrl = "~/resources/pdf/AdherenceFlashcard.pdf",
                    ListOrder = 1,
                    // Audit
                    CreatedBy = "Tyson Davila",
                    CreatedDate = DateTime.Now.AddDays(-1),
                    CreatedById = new Guid(),
                    CreatedUtcDate = DateTime.UtcNow,
                    ModifiedBy = "Bob Villa",
                    ModifiedDate = DateTime.Now,
                    ModifiedById = new Guid(),
                    ModifiedUtcDate = DateTime.UtcNow 
                },
                new Resource
                { 
                    // Properties
                    ResourceId = 16,
                    Title = "The economic impact of diabetes",
                    Description = "The substantial costs associated with diabetes",
                    Category = "Health Economics &amp; Outcomes Research",
                    IsPublished = true,
                    ThumbnailName = "thmbEconomicImpact",
                    ThumbnailExt = ".jpg",
                    ThumbnailMimeType = "image/jpeg",
                    ThumbnailUrl = "~/resources/pdf/thmb/thmbEconomicImpact.jpg",
                    ResourceName = "EconomicImpact",
                    ResourceExt = ".pdf",
                    ResourceMimeType = "application/pdf",
                    ResourceUrl = "~/resources/pdf/EconomicImpact.pdf",
                    ListOrder = 2,
                    // Audit
                    CreatedBy = "Tyson Davila",
                    CreatedDate = DateTime.Now.AddDays(-1),
                    CreatedById = new Guid(),
                    CreatedUtcDate = DateTime.UtcNow,
                    ModifiedBy = "Bob Villa",
                    ModifiedDate = DateTime.Now,
                    ModifiedById = new Guid(),
                    ModifiedUtcDate = DateTime.UtcNow 
                },
                new Resource
                { 
                    // Properties
                    ResourceId = 17,
                    Title = "Ever feel as if the costs of diabetes management just keep coming around?",
                    Description = "A quick look at the costs associated with hypoglycemia",
                    Category = "Health Economics &amp; Outcomes Research",
                    IsPublished = true,
                    ThumbnailName = "thmbDiabetesCosts",
                    ThumbnailExt = ".jpg",
                    ThumbnailMimeType = "image/jpeg",
                    ThumbnailUrl = "~/resources/pdf/thmb/thmbDiabetesCosts.jpg",
                    ResourceName = "DiabetesCosts",
                    ResourceExt = ".pdf",
                    ResourceMimeType = "application/pdf",
                    ResourceUrl = "~/resources/pdf/DiabetesCosts.pdf",
                    ListOrder = 3,
                    // Audit
                    CreatedBy = "Tyson Davila",
                    CreatedDate = DateTime.Now.AddDays(-1),
                    CreatedById = new Guid(),
                    CreatedUtcDate = DateTime.UtcNow,
                    ModifiedBy = "Bob Villa",
                    ModifiedDate = DateTime.Now,
                    ModifiedById = new Guid(),
                    ModifiedUtcDate = DateTime.UtcNow 
                },
                new Resource
                { 
                    // Properties
                    ResourceId = 18,
                    Title = "America's diabetes crisis",
                    Description = "An overview of the prevalence and costs burden of diabetes",
                    Category = "Health Economics &amp; Outcomes Research",
                    IsPublished = true,
                    ThumbnailName = "thmbDiabetesCrisis",
                    ThumbnailExt = ".jpg",
                    ThumbnailMimeType = "image/jpeg",
                    ThumbnailUrl = "~/resources/pdf/thmb/thmbDiabetesCrisis.jpg",
                    ResourceName = "DiabetesCrisis",
                    ResourceExt = ".mp4",
                    ResourceMimeType = "video/mp4",
                    ResourceUrl = "~/resources/pdf/DiabetesCrisis.mp4",
                    ListOrder = 4,
                    // Audit
                    CreatedBy = "Tyson Davila",
                    CreatedDate = DateTime.Now.AddDays(-1),
                    CreatedById = new Guid(),
                    CreatedUtcDate = DateTime.UtcNow,
                    ModifiedBy = "Bob Villa",
                    ModifiedDate = DateTime.Now,
                    ModifiedById = new Guid(),
                    ModifiedUtcDate = DateTime.UtcNow 
                },
                new Resource
                { 
                    // Properties
                    ResourceId = 19,
                    Title = "America's diabetes crisis: screening and treatment",
                    Description = "Information and recommendations on effective diabetes management for your member population",
                    Category = "Health Economics &amp; Outcomes Research",
                    IsPublished = true,
                    ThumbnailName = "thmbCrisisScreening",
                    ThumbnailExt = ".jpg",
                    ThumbnailMimeType = "image/jpeg",
                    ThumbnailUrl = "~/resources/pdf/thmb/thmbCrisisScreening.jpg",
                    ResourceName = "CrisisScreening",
                    ResourceExt = ".mp4",
                    ResourceMimeType = "video/mp4",
                    ResourceUrl = "~/resources/pdf/CrisisScreening.mp4",
                    ListOrder = 5,
                    // Audit
                    CreatedBy = "Tyson Davila",
                    CreatedDate = DateTime.Now.AddDays(-1),
                    CreatedById = new Guid(),
                    CreatedUtcDate = DateTime.UtcNow,
                    ModifiedBy = "Bob Villa",
                    ModifiedDate = DateTime.Now,
                    ModifiedById = new Guid(),
                    ModifiedUtcDate = DateTime.UtcNow 
                },
                // CONTINUITY OF CARE
                new Resource
                { 
                    // Properties
                    ResourceId = 20,
                    Title = "Transitions overview",
                    Description = "An introduction to Novo Nordisk's Transitions program, including descriptions of available resources for your providers and members",
                    Category = "Continuity Of Care",
                    IsPublished = true,
                    ThumbnailName = "thmbTransitionsOverview",
                    ThumbnailExt = ".jpg",
                    ThumbnailMimeType = "image/jpeg",
                    ThumbnailUrl = "~/resources/pdf/thmb/thmbTransitionsOverview.jpg",
                    ResourceName = "TransitionsOverview",
                    ResourceExt = ".pdf",
                    ResourceMimeType = "application/pdf",
                    ResourceUrl = "~/resources/pdf/TransitionsOverview.pdf",
                    ListOrder = 0,
                    // Audit
                    CreatedBy = "Tyson Davila",
                    CreatedDate = DateTime.Now.AddDays(-1),
                    CreatedById = new Guid(),
                    CreatedUtcDate = DateTime.UtcNow,
                    ModifiedBy = "Bob Villa",
                    ModifiedDate = DateTime.Now,
                    ModifiedById = new Guid(),
                    ModifiedUtcDate = DateTime.UtcNow 
                },
                new Resource
                { 
                    // Properties
                    ResourceId = 21,
                    Title = "Transitions: member checklist",
                    Description = "Questions for members to ask their healthcare providers before being discharged from the hospital",
                    Category = "Continuity Of Care",
                    IsPublished = true,
                    ThumbnailName = "thmbMemberChecklist",
                    ThumbnailExt = ".jpg",
                    ThumbnailMimeType = "image/jpeg",
                    ThumbnailUrl = "~/resources/pdf/thmb/thmbMemberChecklist.jpg",
                    ResourceName = "MemberChecklist",
                    ResourceExt = ".pdf",
                    ResourceMimeType = "application/pdf",
                    ResourceUrl = "~/resources/pdf/MemberChecklist.pdf",
                    ListOrder = 1,
                    // Audit
                    CreatedBy = "Tyson Davila",
                    CreatedDate = DateTime.Now.AddDays(-1),
                    CreatedById = new Guid(),
                    CreatedUtcDate = DateTime.UtcNow,
                    ModifiedBy = "Bob Villa",
                    ModifiedDate = DateTime.Now,
                    ModifiedById = new Guid(),
                    ModifiedUtcDate = DateTime.UtcNow 
                },
                new Resource
                { 
                    // Properties
                    ResourceId = 22,
                    Title = "Transitions: healthcare provider checklist",
                    Description = "A tool that providers can use to capture a member's information at the time of hospital admission",
                    Category = "Continuity Of Care",
                    IsPublished = true,
                    ThumbnailName = "thmbProviderChecklist",
                    ThumbnailExt = ".jpg",
                    ThumbnailMimeType = "image/jpeg",
                    ThumbnailUrl = "~/resources/pdf/thmb/thmbProviderChecklist.jpg",
                    ResourceName = "ProviderChecklist",
                    ResourceExt = ".pdf",
                    ResourceMimeType = "application/pdf",
                    ResourceUrl = "~/resources/pdf/ProviderChecklist.pdf",
                    ListOrder = 2,
                    // Audit
                    CreatedBy = "Tyson Davila",
                    CreatedDate = DateTime.Now.AddDays(-1),
                    CreatedById = new Guid(),
                    CreatedUtcDate = DateTime.UtcNow,
                    ModifiedBy = "Bob Villa",
                    ModifiedDate = DateTime.Now,
                    ModifiedById = new Guid(),
                    ModifiedUtcDate = DateTime.UtcNow 
                },
                new Resource
                { 
                    // Properties
                    ResourceId = 23,
                    Title = "Why transitions?",
                    Description = "A set of studies that summarize the benefits of supporting your members as they transition between care settings",
                    Category = "Continuity Of Care",
                    IsPublished = true,
                    ThumbnailName = "thmbTransitions",
                    ThumbnailExt = ".jpg",
                    ThumbnailMimeType = "image/jpeg",
                    ThumbnailUrl = "~/resources/pdf/thmb/thmbTransitions.jpg",
                    ResourceName = "Transitions",
                    ResourceExt = ".pdf",
                    ResourceMimeType = "application/pdf",
                    ResourceUrl = "~/resources/pdf/Transitions.pdf",
                    ListOrder = 3,
                    // Audit
                    CreatedBy = "Tyson Davila",
                    CreatedDate = DateTime.Now.AddDays(-1),
                    CreatedById = new Guid(),
                    CreatedUtcDate = DateTime.UtcNow,
                    ModifiedBy = "Bob Villa",
                    ModifiedDate = DateTime.Now,
                    ModifiedById = new Guid(),
                    ModifiedUtcDate = DateTime.UtcNow 
                }
            }.AsQueryable());
            ninjectKernel.Bind<IResourceRepository>().ToConstant(mock.Object);
            #endregion
        }
    }
}