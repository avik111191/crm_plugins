using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
            nazi_b_22_entity b_22= (nazi_b_22_entity)context.InputParameters["Target"];
            nazi_b_23_entity b_23= new nazi_b_23_entity();
            b_23.nazi_name=b_22.nazi_name;
            b_23.nazi_at3=b_22.nazi_X;
            b_23.OwnerId=b_22.OwnerId;

            Guid b_23_id = service.Create(b_23);
            service.
            

        }
    }
}
