using EnvDTE;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TemplateWizard;
using System.Windows.Forms;

namespace QuokkaDevVSIXTemplates
{
    /// <summary>
    /// Wizard for aggregate creation
    /// </summary>
    public class AggregateWizard : IWizard
    {
        private bool IncludeIRepo = false;
        private bool IncludeEvents = false;
        private bool IncludeFactory = false;
        private bool IncludeSpec = false;

        public void BeforeOpeningFile(ProjectItem projectItem)
        {
        }

        public void ProjectFinishedGenerating(Project project)
        {
        }

        public void ProjectItemFinishedGenerating(ProjectItem projectItem)
        {            
        }

        public void RunFinished()
        {
        }

        public void RunStarted(object automationObject, Dictionary<string, string> replacementsDictionary, WizardRunKind runKind, object[] customParams)
        {
            using (NewAggregateForm form = new NewAggregateForm())
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    IncludeIRepo = form.IncludeIRepo;
                    IncludeEvents = form.IncludeEvents;
                    IncludeFactory = form.IncludeFactory;
                    IncludeSpec = form.IncludeSpec;
                }
                else
                {
                    throw new WizardCancelledException();
                }                
            }
        }

        public bool ShouldAddProjectItem(string filePath)
        { 
            if(filePath.EndsWith("Repository.cs", StringComparison.InvariantCultureIgnoreCase) && !IncludeIRepo)
            {
                return false;
            }
            else if (filePath.EndsWith("Events.cs", StringComparison.InvariantCultureIgnoreCase) && !IncludeEvents)
            {
                return false;
            }
            else if (filePath.EndsWith("Factory.cs", StringComparison.InvariantCultureIgnoreCase) && !IncludeFactory)
            {
                return false;
            }
            else if (filePath.EndsWith("Specifications.cs", StringComparison.InvariantCultureIgnoreCase) && !IncludeSpec)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

    }
}
