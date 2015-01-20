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
        Entity con;

        ColumnSet colunmset = new ColumnSet(true);
        Guid salesperson = new Guid();


        public void Execute(IServiceProvider serviceProvider)
        {
            provider=serviceProvider;
            _context_service();
            _createtask_task_entity();
            _create_email_atvity();
            _salesperson_userid();
            _assign_activities();

      

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
            con = (Entity)context.InputParameters["Target"];
        }

        private async void _createtask_task_entity()
        {
            await create__task_entity();
        }

        private async System.Threading.Tasks.Task create__task_entity()
        {
            crm_ddl_namespace.Task task = new crm_ddl_namespace.Task();
            task.Subject = (string)con["name"];
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
            email.Subject = (string)con["name"];
            email.Description = "nazi nigger created this task";
            emailid_ = (Guid)service.Create(email);
            ActivityParty activityparty=new ActivityParty();
            SystemUser systemuser =new SystemUser();
            EntityReference from = new EntityReference("systemuser", context.UserId);
            activityparty.PartyId = from;

            email.From = (System.Collections.Generic.IEnumerable<crm_ddl_namespace.ActivityParty>)activityparty;

            emailid_ =(Guid)service.Create(email);
           
             
           
        }

        private async void  _salesperson_userid()
        {
         await salesperson_userid();
        }
       private async System.Threading.Tasks.Task salesperson_userid()
       {
          QueryExpression query = new QueryExpression(systemuser.LogicalName);
          query.ColumnSet = colunmset;
          query.Criteria.AddCondition(systemuser.LogicalName,"domainname", ConditionOperator.Equal,           "sales_person@damnidiot.onmicrosoft.com");
          EntityCollection systemusers = service.RetrieveMultiple(query);
           foreach (var a in systemusers.Entities)
           { salesperson = (Guid)a["systemuserid"]; }
       }
        private async void _assign_activities()
       {
         await assign_activities();
       }
       private async System.Threading.Tasks.Task assign_activities()
       {
          Email email_ = new Email();
          crm_ddl_namespace.Task task_ = new crm_ddl_namespace.Task();
          AssignRequest assign = new AssignRequest
             {
                 Assignee = new EntityReference(SystemUser.EntityLogicalName,salesperson),
                 Target = new EntityReference(task_.LogicalName,taskss)
             };
         service.Execute(assign);

          AssignRequest assign = new AssignRequest
             {
                 Assignee = new EntityReference(SystemUser.EntityLogicalName,salesperson),
                 Target = new EntityReference(email_.LogicalName,emailid_)
             };
         service.Execute(assign);
       }
    }
}

