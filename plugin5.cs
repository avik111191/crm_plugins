using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Crm.Sdk;
using Microsoft.Crm.Sdk.Messages;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using Microsoft.Xrm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Linq;
using System.Threading.Tasks;
using System.Threading;
using crm_ddl_namespace;

namespace plugin5
{
    public class Class1:IPlugin
    {
        IPluginExecutionContext context;
        IOrganizationServiceFactory factory;
        IOrganizationService service;
        IServiceProvider provider;
        static bool context_service;
        Guid taskss = new Guid();
        Guid emailid_ = new Guid();

        public void Execute(IServiceProvider serviceProvider)
        {
            provider=serviceProvider;
            _context_service();
            _createtask_task_entity();
            _create_email_atvity();
      

        }

        private async void _context_service()
        {
            await context_service_();
        }

        private async System.Threading.Tasks.Task context_service_()
        {
            context = (IPluginExecutionContext)provider.GetService(typeof(IPluginExecutionContext));
            factory = (IOrganizationServiceFactory)provider.GetService(typeof(IOrganizationServiceFactory));
            service = factory.CreateOrganizationService(context.UserId);
        }

        private async void _createtask_task_entity()
        {
            await create__task_entity();
        }

        private async System.Threading.Tasks.Task create__task_entity()
        {
            crm_ddl_namespace.Task task = new crm_ddl_namespace.Task();
            task.Subject = "Heil Hittler";
            task.Description = "nazi nigger created this task";

            taskss=service.Create(task);

        }

        private async void _create_email_atvity()
        {
            await create_email_atvity();
        }

        private async System.Threading.Tasks.Task create_email_atvity()
        {
            Email email = new Email();
            email.ToRecipients = "avik111191@gmail.com";
            //email.To=
            //email.From=
            email.Subject = "Heil Hitler";
            email.Description = "created by nazi nigger";
            emailid_ = (Guid)service.Create(email);
           
        }
    }
}