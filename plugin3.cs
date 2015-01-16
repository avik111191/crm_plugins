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

using crm_ddl_namespace;


namespace plugin3
{
    public class Clas : IPlugin
    {
        bool flag1 = false, flag2 = false;
          IPluginExecutionContext context;
          IOrganizationServiceFactory factory;
          IOrganizationService service;
          IServiceProvider Provider;
          ColumnSet colunmset = new ColumnSet(true);
          Guid salesperson = new Guid();
          SystemUser systemuser = new SystemUser();
          nazi_b_22_entity b22 = new nazi_b_22_entity();
          Entity entity;
         public void Execute(IServiceProvider serviceProvider)
        {
            Provider = serviceProvider;
            cal_initfun();
            cal_ser();
            if (flag1 == false)
                throw new InvalidPluginExecutionException("email not present");
            else if (flag2 == false)
                throw new InvalidPluginExecutionException("not a valid email");
            else
                flag1 = flag2 = true;
 

        }



         private async void cal_initfun()
         {
             await initfunc();
         }

         private async System.Threading.Tasks.Task initfunc()
         {
             context = (IPluginExecutionContext)Provider.GetService(typeof(IPluginExecutionContext));
             factory = (IOrganizationServiceFactory)Provider.GetService(typeof(IOrganizationServiceFactory));
             service = factory.CreateOrganizationService(context.UserId);
         }

         private async void cal_ser()
         {
             await servi();
         }

         private async System.Threading.Tasks.Task servi()
         {

            
            QueryExpression query = new QueryExpression(systemuser.LogicalName);
            query.ColumnSet = colunmset;
            query.Criteria.AddCondition(systemuser.LogicalName,"domainname", ConditionOperator.Equal, "sales_person@damnidiot.onmicrosoft.com");
            EntityCollection systemusers = service.RetrieveMultiple(query);
            foreach (var a in systemusers.Entities)
            { salesperson = (Guid)a["systemuserid"]; }
             entity = (Entity)context.InputParameters["Target"];
           if(entity["nazi_email"]!=null)
           {
               flag1 = true;
               string st = (string)entity["nazi_email"];
               bool x=st.Contains("@"),y=st.Contains(".com");
               if (x & y == true)
               {
                   cal_ass();
                   flag2 = true;
               }
               
           }
         }

         private async void cal_ass()
         {
             await ass();
         }

         private async System.Threading.Tasks.Task ass()
         {
             
             AssignRequest assign = new AssignRequest
             {
                 Assignee = new EntityReference(SystemUser.EntityLogicalName,
                     salesperson),
                 Target = new EntityReference(entity.LogicalName,
                     entity.Id)
             };
             service.Execute(assign);
         }
    }
}
