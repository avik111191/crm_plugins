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


                      SystemUser systemuser=(SystemUser)serv.Retrieve("systemuser",con.UserId,new ColumnSet(true));
           SystemUser inituser =(SystemUser)serv.Retrieve("systemuser",con.InitiatingUserId,new ColumnSet(true));
            SystemUserRoles rol = new SystemUserRoles();
            Role rile = new Role();
           
          
          
               ColumnSet col=new ColumnSet();
                col.AllColumns=true;
            QueryExpression query = new QueryExpression(rol.LogicalName);        //string s = rol.LogicalName;
           query.ColumnSet=col;
           query.Criteria.AddCondition(rol.LogicalName, "systemuserid", ConditionOperator.Equal, inituser.Id);
           //SystemUser sys=new SystemUser(); 
           //ColumnSet col=new ColumnSet();
           //col.AddColumn("domainname");
           //col.AddColumn("OrganizationId");
          
           Guid role_id=new Guid();
           EntityCollection rols = serv.RetrieveMultiple(query);
           foreach(var p in rols.Entities){role_id=(Guid)p["roleid"] ;}               //rol.RoleId
           QueryExpression roleq = new QueryExpression(rile.LogicalName);
           roleq.ColumnSet=col;
           roleq.Criteria.AddCondition(rile.LogicalName,"roleid",ConditionOperator.Equal,role_id);
           EntityCollection role = serv.RetrieveMultiple(roleq);
           string tem=null;
           foreach(var s in role.Entities){tem=(string)s["name"];}                   


          // Guid g=(Guid)systemuser.OrganizationId;
           Organization organisation = (Organization)serv.Retrieve("organization",(Guid)systemuser.OrganizationId, new ColumnSet(true));
          // systemuser.
           Entity entity = (Entity)con.InputParameters["Target"];
           entity["nazi_inituser"] = inituser.DomainName;
           entity["nazi_pluginname"] = systemuser.DomainName;
           entity["nazi_orgname"] = organisation.Name;
           entity["nazi_role"] = tem;
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
