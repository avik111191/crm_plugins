using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Client;
using Microsoft.Xrm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Query;
using Microsoft.Xrm.Sdk.Linq;

using Microsoft.Crm.Sdk.Messages;

using System.Threading;
using System.Threading.Tasks;

using avikcompany_dll;

namespace temp
{
    public class Class1:IPlugin
    {
        public void Execute(IServiceProvider serviceProvider) 
        {
            IPluginExecutionContext context = (IPluginExecutionContext)serviceProvider.GetService(typeof(IPluginExecutionContext));
            IOrganizationServiceFactory serviceFactory = (IOrganizationServiceFactory)serviceProvider.GetService(typeof(IOrganizationServiceFactory));
            IOrganizationService service = serviceFactory.CreateOrganizationService(context.UserId);
            Lead lead = new Lead();
             ColumnSet columnSet =new ColumnSet(true);
             Stack<bool> flag =new Stack<bool>(); 
            Entity leads=(Entity)context.InputParameters["Target"];
            Guid lead_guid=(Guid)leads.Id;
            //lead.e
           // lead = (avikcompany_dll.Lead)context.InputParameters["Target"];
            Contact contact = new Contact();
             DuplicateRule duplicatedetectionrule= new DuplicateRule();
           
            //duplicatedetectionrule.BaseEntityName
            //duplicatedetectionrule.MatchingEntityName
            QueryExpression query = new QueryExpression();
            query.EntityName=duplicatedetectionrule.LogicalName;
            query.Criteria.AddCondition("baseentityname",ConditionOperator.Equal,lead.LogicalName);
            query.Criteria.AddCondition("matchingentityname",ConditionOperator.Equal,contact.LogicalName);

            //duplicatedetectionrule.
            EntityCollection duplicaterule=service.RetrieveMultiple(query);
           // var b = 
            if(duplicaterule!=null)
            {
                var ent = from a in context.InputParameters
                          where a.Key.Contains("Target")
                          select a.Value;
                
                var guid = from dup in duplicaterule.Entities
                           select dup.Id ;


                Guid guid_=new Guid();
                foreach(var p in guid)
                { guid_ = (Guid)p; }
                DuplicateRuleCondition duplicateditection_rule_conditions = new DuplicateRuleCondition();
                //duplicateditection_rule_conditions.MatchingAttributeName;
                //duplicateditection_rule_conditions.RegardingObjectId
                //duplicateditection_rule_conditions.BaseAttributeName
                QueryExpression duprul = new QueryExpression(duplicateditection_rule_conditions.LogicalName);
                duprul.ColumnSet = columnSet;
                duprul.Criteria.AddCondition("regardingobjectid",ConditionOperator.Equal,guid_);

                EntityCollection duplicateditection_rule_conditions_entity = service.RetrieveMultiple(duprul);

                var duplict_ru_con = from aa2 in duplicateditection_rule_conditions_entity.Entities select aa2;
                Stack<string> conds_m = new Stack<string>();
                Stack<string> conds_b = new Stack<string>();
                foreach(var pp1 in duplict_ru_con)
                {
                    //var c = from ppp in pp1.Attributes
                    //        where ppp.Key.Equals("matchingattributename") 
                    //           select  ppp.Value;
                    //var d = from ppp in pp1.Attributes
                    //        where ppp.Key.Equals("baseattributename")
                    //        select ppp.Value;

                    conds_m.Push((string)pp1["matchingattributename"]);
                    conds_b.Push((string)pp1["baseattributename"]);
                }

                while(conds_m.Count>=0)
                {
                    string attribute_1 = conds_m.Pop();
                    string attribute_2 = conds_b.Pop();
                    string leads_att= (string)leads[attribute_2];
                    //string 

                    QueryExpression con_query = new QueryExpression();
                    con_query.EntityName = contact.LogicalName;
                    con_query.ColumnSet = columnSet;
                    con_query.Criteria.AddCondition(attribute_1, ConditionOperator.Equal, leads_att);
                    EntityCollection contacts = service.RetrieveMultiple(con_query);
                    if (contacts.Entities.Count > 0) 
                    {
                        SetStateRequest setStateRequest = new SetStateRequest()
                        {
                            EntityMoniker = new EntityReference { Id = lead_guid, LogicalName = "lead" },
                            State = new OptionSetValue(2),
                            Status = new OptionSetValue(7)
                        };
                        service.Execute(setStateRequest);
                    }
                
                
                
                }

                                     

                //StringComparison stcmp= new StringComparison();
                //var natching_attribute = from a2 in duplicateditection_rule_conditions_entity.Attributes
                //                         where a2.Key.Equals("matchingattributename", StringComparison.Ordinal)
                //                         select a2.Value;

                //string matching_attribute =(string)duplicateditection_rule_conditions["matchingattributename"];

                //string leads_email= (string)leads["emailaddress1"];
                //QueryExpression con_query = new QueryExpression();
                //con_query.EntityName = contact.LogicalName;
                //con_query.ColumnSet = columnSet;
                //con_query.Criteria.AddCondition("emailaddress1", ConditionOperator.Equal, leads_email);
                //EntityCollection contacts = service.RetrieveMultiple(con_query);
                //if(contacts.Entities.Count ==0)
                //{
                //    QualifyLeadRequest qualify_lead_request = new QualifyLeadRequest();
                //    qualify_lead_request.CreateAccount = true;
                //    qualify_lead_request.CreateContact = true;
                //    qualify_lead_request.LeadId = new EntityReference(lead.LogicalName,lead_guid);
                //    qualify_lead_request.Status = new OptionSetValue(1); //new

                //    QualifyLeadResponse qualify_lead_response = (QualifyLeadResponse)service.Execute(qualify_lead_request);
                    
                //}
                //else
                //{

                //}

                //CreateRequest createrequest = new CreateRequest();
                //createrequest.Target = lead;
                //createrequest.Parameters.Add("SuppressDuplicateDetection", false);

                //CreateResponse createresponse = (CreateResponse)service.Execute(createrequest);


            }
        
        }
    }
}
