using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using Microsoft.Xrm.Sdk.Messages;
using plugin1;
using crm_ddl_namespace;

namespace plugin1
{
    public class Class1:IPlugin
    {
       public void Execute(IServiceProvider serviceProvider)
        {
          // ITracingService 
            IPluginExecutionContext con = (IPluginExecutionContext)serviceProvider.GetService(typeof(IPluginExecutionContext));
            IOrganizationServiceFactory orgfact =(IOrganizationServiceFactory)serviceProvider.GetService(typeof(IOrganizationServiceFactory));
            IOrganizationService serv = (IOrganizationService)orgfact.CreateOrganizationService(con.UserId);
           
          
           //SystemUser sys=new SystemUser(); 
           //ColumnSet col=new ColumnSet();
           //col.AddColumn("domainname");
           //col.AddColumn("OrganizationId");
           SystemUser systemuser=(SystemUser)serv.Retrieve("systemuser",con.UserId,new ColumnSet(true)); 
           SystemUser inituser =(SystemUser)serv.Retrieve("systemuser",con.InitiatingUserId,new ColumnSet(true));
          // Guid g=(Guid)systemuser.OrganizationId;
           Organization organisation = (Organization)serv.Retrieve("organization",(Guid)systemuser.OrganizationId, new ColumnSet(true));

           Entity entity = (Entity)con.InputParameters["Target"];
           entity["nazi_inituser"] = inituser.DomainName;
           entity["nazi_pluginname"] = systemuser.DomainName;
           entity["nazi_orgname"] = organisation.Name;
           //nazi_a_22_entity a_22 = (nazi_a_22_entity)entity;
           //a_22.nazi_inituser = inituser.DomainName;
           //a_22.nazi_pluginname = systemuser.DomainName;
           //a_22.nazi_orgname = organisation.Name;
           try
           {
              serv.Update(entity);
           }
           catch(Exception ep)
           {
               throw new InvalidPluginExecutionException(ep.Message);

           }




           }

      
    }
}
