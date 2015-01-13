using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using Microsoft.Xrm.Sdk.Messages;

using crm_ddl_namespace;

namespace plugin2
{
    public class Class1:IPlugin
    {
        public void Execute(IServiceProvider serviceProvider)
        {
            //ITracingService trace;
            IPluginExecutionContext context = (IPluginExecutionContext)serviceProvider.GetService(typeof(IPluginExecutionContext));
            IOrganizationServiceFactory factory = (IOrganizationServiceFactory)serviceProvider.GetService(typeof(IOrganizationServiceFactory));
            IOrganizationService service = factory.CreateOrganizationService(context.UserId);
            Entity b_22= (Entity)context.InputParameters["Target"];
            ColumnSet c= new ColumnSet();
            c.AllColumns=true;
            Guid g = new Guid("{0314349B-F69A-E411-80E3-80C16E644B9A}");
            //Entity temp = service.Retrieve("nazi_b_22_entity",g,c); ;

           // b_22["statecode"] = "inactive";
            nazi_b_23_entity b_23= new nazi_b_23_entity();
            b_23.nazi_name=(string)b_22["nazi_name"];
            //b_23.nazi_at3=(string)b_22["nazi_x"];
            b_23.OwnerId=(EntityReference)b_22["ownerid"];
            var p = b_22["statecode"];
            var q = b_22["statuscode"];
           // b_22.statecode = new OptionSetValue((int));
            Guid b_23_id = service.Create(b_23);
            b_22["nazi_name"] = "changed";
            b_22["statecode"] = new OptionSetValue(1);
            service.Update(b_22);

            
            

        }
    }
}
