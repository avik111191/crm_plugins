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

namespace plugin6
{
    public class Class1 : IPlugin
    {
        IPluginExecutionContext context;
        IOrganizationServiceFactory factory;
        IOrganizationService service;
        IServiceProvider provider;
        static bool context_service;
        Entity con;
        ColumnSet colunmset = new ColumnSet(true);
        Guid salesperson = new Guid();

        public void Execute(IServiceProvider serviceProvider)
        {
            provider = serviceProvider;
            _context_service();
            _retrive_contact_entity();
            //_create_email_atvity();
            //_salesperson_userid();
            //_assign_activities();
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
        private async void _retrive_contact_entity()
        {
            await retrive_contact_entity();
        }
        private async System.Threading.Tasks.Task retrive_contact_entity()
        {
            Account ent=(crm_ddl_namespace.Account)con;
            Contact contact = new Contact();
            string emailadd =(string)con["emailaddress1"];
            
            QueryExpression query = new QueryExpression();
            query.EntityName = contact.LogicalName;
            query.ColumnSet = colunmset;
            query.Criteria.AddCondition(contact.LogicalName, "emailaddress1", ConditionOperator.Equal,emailadd);
            
            EntityCollection contacts = service.RetrieveMultiple(query);
            

        }
    }
}
