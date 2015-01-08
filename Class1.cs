using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//<summary>required for Iplugin </summary> 
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Query;
//<summary>required for cremconection</summary>
//using Microsoft.Xrm.Client;
//using Microsoft.Xrm.Client.Services;




namespace my_bloody_plugin
{
    public class Class1:IPlugin
    {
        public void Execute(IServiceProvider serviceProvider)
        {
            Entity entity =null;
            string fname=null;
            //string connectionString = "Url=https://damnidiot.crm5.dynamics.com; Username=mr.blitheringidiot@damnidiot.onmicrosoft.com; Password=Nazi111191;";
            ITracingService trace = (ITracingService)serviceProvider.GetService(typeof(ITracingService));
            IPluginExecutionContext context = (IPluginExecutionContext)serviceProvider.GetService(typeof(IPluginExecutionContext));

            IOrganizationServiceFactory serviceFactory = (IOrganizationServiceFactory)serviceProvider.GetService(typeof(IOrganizationServiceFactory));
            IOrganizationService service = serviceFactory.CreateOrganizationService(context.UserId);
            
            if (context.InputParameters.Contains("Target") &&
               context.InputParameters["Target"] is Entity)
            {
                // Obtain the target entity from the input parameters.
               entity = (Entity)context.InputParameters["Target"];
             // string fnamef =(string)entity["firstname"];
                //throw new InvalidPluginExecutionException("my  bloody plugin is working");
            }
            //  entity["firstname"] = "sdfsad";
            //var pq = from p in entity.Attributes
            //         where entity.Attributes.Keys.Equals("firstname")
            //         select new { name = entity.Attributes.Values };
            
            foreach (var p in entity.Attributes)
            {
                if (p.Key.Equals("firstname"))
                {
                    fname = (string)p.Value;
                    break;
                }
            }

            //CrmConnection con = CrmConnection.Parse(connectionString);
            //OrganizationService org = new OrganizationService(con);
            QueryExpression query = new QueryExpression(entity.LogicalName);
            query.ColumnSet.AllColumns = true;
            query.Criteria.AddCondition(entity.LogicalName, "firstname", ConditionOperator.Equal, fname);
            EntityCollection ent = service.RetrieveMultiple(query);

            if (ent != null)
            {
                throw new InvalidPluginExecutionException("you can't  create it because you all readty have an conatact with same name");
            }
            


        }
    }
}
