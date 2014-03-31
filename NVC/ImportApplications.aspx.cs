using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using Oracle.ManagedDataAccess.Client;

using ActiveUp.Net.Mail;
using WebApplication1.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;



namespace WebApplication1
{



    public partial class ImportApplications : Page
    {

        public string trainSN;
        public int AppNo;
        protected void TestMail()
        {
            MailRepository rep = new MailRepository("imap.gmail.com", 993, true, @"dhammashringaxml@gmail.com", @"dhamma@xml");

            foreach (Message email in rep.GetUnreadMails("Inbox"))
            {
                Response.Write(string.Format("<p>{0}: {1}</p><p>{2}</p>", email.From, email.Subject, email.BodyHtml.Text));
                if (email.Attachments.Count > 0)
                {
                    foreach (MimePart attachment in email.Attachments)
                    {
                        //attachment.
                        //  attachment.TextContent
                        Response.Write(string.Format("<p>Attachment: {0} {1}</p><br/>{2}", attachment.ContentName, attachment.ContentType.MimeType, attachment.TextContent));
                    }
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {

            //GetTrainSN(Convert.ToDateTime("2012-04-14"));

            if (!string.IsNullOrEmpty(User.Identity.GetUserId()))
            {
                if (!IsPostBack)
                    ShowApplications();


                //TestMail();
                // ConnectingToOracle();
            }
            else
            {
                Response.Redirect("~/Account/Login");
            }
        }


        protected void ShowApplications()
        {
            string connectionString = GetConnectionString();
            using (OracleConnection connection = new OracleConnection())
            {
                connection.ConnectionString = connectionString;
                connection.Open();
                OracleCommand command = connection.CreateCommand();
                string sql = @"SELECT TRAIN_S_N AS ""Course No"",TO_CHAR(START_DATE,'MON-DD-YYYY') AS ""Start Date"",TO_CHAR(TILL_DATE,'MON-DD-YYYY') AS ""End Date"",APP_FNAME AS ""First Name"",APP_LNAME AS ""Last Name"", APP_AGE AS ""Age"",APP_HPH_NO AS ""Home Phone"",APP_OPH_NO AS ""Other Phone"",APP_EMAIL AS ""Email"",APP_ADDR AS ""Address"",APP_BUSINESS AS ""Occupation"", OLD_Y_N AS ""Old"",COURSE_TYPE AS ""Course Type"" FROM T_Application WHERE START_DATE> CURRENT_DATE ORDER BY START_DATE,APP_FNAME";
                command.CommandText = sql;

               
                OracleDataReader reader = command.ExecuteReader();
                gvApplication.DataSource = reader;
                gvApplication.DataBind();
            }
        }
        private string GetConnectionString()
        {
            String connString = "DATA SOURCE=NVC;PASSWORD=VIPA;USER ID=VIPA;";
            return connString;
        }


        public Dictionary<string, string> dictApplication = new Dictionary<string, string>();
        private void InsertApplication()
        {

            #region Database Fields
            //            TABLE T_Application
            //Name                                      Null?    Type                        
            //----------------------------------------- -------- ----------------------------
            //REG_NO                                             VARCHAR2(15)                
            //APP_NO                                    NOT NULL VARCHAR2(15)                
            //START_DATE                                NOT NULL DATE                        
            //START_NDATE                                        VARCHAR2(11)                
            //TILL_DATE                                          DATE                        
            //TILL_NDATE                                         VARCHAR2(11)                
            //APP_FNAME                                          VARCHAR2(50)                
            //APP_MNAME                                          VARCHAR2(50)                
            //APP_LNAME                                          VARCHAR2(50)                
            //APP_ADDR                                           VARCHAR2(100)               
            //APP_BUSINESS                                       VARCHAR2(50)                
            //APP_EDU                                            VARCHAR2(50)                
            //APP_HPH_NO                                         VARCHAR2(50)                
            //APP_OPH_NO                                         VARCHAR2(50)                
            //APP_NPH_NO                                         VARCHAR2(50)                
            //APP_AGE                                            NUMBER(3)                   
            //APP_GENDER                                         VARCHAR2(10)                
            //APP_DOB                                            DATE                        
            //APP_NDOB                                           VARCHAR2(11)                
            //APP_EMAIL                                          VARCHAR2(50)                
            //APP_WITH_F_R                                       VARCHAR2(5)                 
            //F_R_NAME                                           VARCHAR2(50)                
            //F_R_RELATION                                       VARCHAR2(50)                
            //APP_LANG_HINDI                                     VARCHAR2(5)                 
            //APP_LANG_ENG                                       VARCHAR2(5)                 
            //APP_LANG_NEP                                       VARCHAR2(5)                 
            //APP_LANG_NEWARY                                    VARCHAR2(5)                 
            //APP_LANG_OTHERS                                    VARCHAR2(50)                
            //ANY_PREV_TRAINING                                  VARCHAR2(5)                 
            //APP_DISABILITY                                     VARCHAR2(5)                 
            //APP_DISAB_DETAIL                                   VARCHAR2(500)               
            //APP_ANY_PREV_EXP                                   VARCHAR2(5)                 
            //APP_PREV_EXP_DETAIL                                VARCHAR2(500)               
            //HOW_U_KNOW_DETAIL                                  VARCHAR2(500)               
            //REF_NAME                                           VARCHAR2(50)                
            //REF_ADDR                                           VARCHAR2(50)                
            //REF_PH_NO                                          VARCHAR2(50)                
            //REF_LAST_TRAIN_PLACE                               VARCHAR2(50)                
            //REF_LAST_TRAIN_DATE                                VARCHAR2(50)                
            //REF_LAST_TRAIN_TEACH_NAME                          VARCHAR2(50)                
            //REF_DATE                                           DATE                        
            //REF_NDATE                                          VARCHAR2(11)                
            //READ_ABOUT_BIPA                                    VARCHAR2(5)                 
            //UNDERSTAND_ABOUT_BIPA                              VARCHAR2(5)                 
            //ABIDE_IN_RULES                                     VARCHAR2(5)                 
            //NO_LEAVE_DURING                                    VARCHAR2(5)                 
            //MAINTAIN_SILENCE                                   VARCHAR2(5)                 
            //LEAVE_OTHER_EXERCISE                               VARCHAR2(5)                 
            //SMOKING_HABIT                                      VARCHAR2(5)                 
            //QUIT_DURING                                        VARCHAR2(5)                 
            //PHYSICAL_ABLE_FOR_TRAIN                            VARCHAR2(5)                 
            //FIRST_TRAN_NDATE                                   VARCHAR2(11)                
            //FIRST_TRAIN_PLACE                                  VARCHAR2(50)                
            //FIRST_TRAIN_TEACH_NAME                             VARCHAR2(50)                
            //LAST_TRAN_NDATE                                    VARCHAR2(11)                
            //LAST_TRAIN_PLACE                                   VARCHAR2(50)                
            //LAST_TRAIN_TEACH_NAME                              VARCHAR2(50)                
            //NO_OF_ONEDAY_TRAIN_CLASS                           NUMBER(3)                   
            //NO_OF_ONDAY_TRAIN_SERV                             NUMBER(3)                   
            //ANY_OTHER_TRAIN_CLASS                              VARCHAR2(100)               
            //ANY_OTHER_TRAIN_SERV                               VARCHAR2(100)               
            //TRAIN_AFTER_SATYA                                  VARCHAR2(5)                 
            //TRAIN_AFTER_SATYA_DETAIL                           VARCHAR2(500)               
            //CONTINUE_AFTER_LAST_TRAIN                          VARCHAR2(5)                 
            //NO_OF_EXERCISE_IN_A_DAY                            NUMBER(3)                   
            //COME_EARLY_ON_REG_DAY                              VARCHAR2(5)                 
            //CAN_YOU_SERVE_IF_NEEDED                            VARCHAR2(5)                 
            //COMING_DATE_IF_PARTIAL                             DATE                        
            //COMING_NDATE_IF_PARTIAL                            VARCHAR2(11)                
            //COMING_TIME_IF_PARTIAL                             VARCHAR2(50)                
            //GOING_DATE_IF_PARTIAL                              DATE                        
            //GOING_NDATE_IF_PARTIAL                             VARCHAR2(11)                
            //GOING_TIME_IF_PARTIAL                              VARCHAR2(50)                
            //REG_DATE                                           DATE                        
            //REG_NDATE                                          VARCHAR2(11)                
            //NAME_FOR_EMERGENCY                                 VARCHAR2(50)                
            //RELATION_FOR_EMERGENCY                             VARCHAR2(20)                
            //ADDR_FOR_EMERGENCY                                 VARCHAR2(50)                
            //PH_NO_FOR_EMERGENCY                                VARCHAR2(50)                
            //TRAIN_S_N                                 NOT NULL VARCHAR2(15)                
            //HOUSE                                              VARCHAR2(50)                
            //P_S_N                                              VARCHAR2(15)                
            //FOREIGN_Y_N                                        VARCHAR2(3)                 
            //LAST_TRAIN_DATE                                    VARCHAR2(20)                
            //FIRST_TRAIN_DATE                                   VARCHAR2(20)                
            //CANCELLED_Y_N                                      VARCHAR2(3)                 
            //OLD_Y_N                                            VARCHAR2(3)                 
            //NO_OF_10_DAYS                                      NUMBER(5)                   
            //NO_OF_10_DAYS_OTHER                                NUMBER(5)                   
            //NO_OF_SATIPATHAN                                   NUMBER(5)                   
            //NO_OF_SPECIAL                                      NUMBER(5)                   
            //NO_OF_20_DAYS                                      NUMBER(5)                   
            //NO_OF_30_DAYS                                      NUMBER(5)                   
            //NO_OF_45_60_DAYS                                   NUMBER(5)                   
            //NO_OF_TEACH_SELF                                   NUMBER(5)                   
            //NO_OF_SERVICE                                      NUMBER(5)                   
            //PERSONAL_IDENTIFICATION_SUMM                       VARCHAR2(500)               
            //MARRIED_Y_N                                        VARCHAR2(3)                 
            //F_H_W_NAME                                         VARCHAR2(100)               
            //F_H_W_EXERCISE                                     VARCHAR2(3)                 
            //EXER_DATE                                          VARCHAR2(15)                
            //NEW_Y_N                                            VARCHAR2(3)                 
            //NEW_Y_N1                                           VARCHAR2(3)                 
            //MONK_Y_N                                           VARCHAR2(3)                 
            //CONFIRMED                                          VARCHAR2(3)                 
            //NEP_NAME
            #endregion Database Fields
            // ReadXmlApplication(xmlApplication);
            string connectionString = GetConnectionString();
            using (OracleConnection connection = new OracleConnection())
            {
                connection.ConnectionString = connectionString;
                connection.Open();
                //Console.WriteLine("State: {0}", connection.State);
                //Console.WriteLine("ConnectionString: {0}",
                //                  connection.ConnectionString);


                foreach (Dictionary<string, string> dictApplication in lstApplications)
                {
                    OracleCommand command = connection.CreateCommand();
//(SELECT CONCAT(:pGender,COUNT(1)+40) FROM T_Application WHERE TRAIN_S_N=(SELECT DISTINCT TRAIN_S_N FROM T_COURSE_START WHERE START_DATE= :pStartDate))
                    //(SELECT DISTINCT TRAIN_S_N FROM T_COURSE_START WHERE START_DATE= :pStartDate)
                    command = new OracleCommand(
@"INSERT INTO T_Application (START_DATE,TILL_DATE,
APP_NO,APP_GENDER, APP_AGE,TRAIN_S_N,APP_FNAME,APP_LNAME
,APP_DOB
,APP_ADDR    
,APP_Email
,APP_HPH_NO  
,APP_OPH_NO
,APP_EDU
,APP_BUSINESS 
,FOREIGN_Y_N 
,OLD_Y_N
,APP_LANG_HINDI
,APP_LANG_ENG
,APP_LANG_NEP
,APP_LANG_NEWARY
, APP_LANG_OTHERS
,ONLINEAPPLICATION_Y_N
--,NEW_Y_N
,COURSE_TYPE
,FIRST_TRAIN_DATE
,FIRST_TRAIN_PLACE
,FIRST_TRAIN_TEACH_NAME
) 
VALUES (:pStartDate,:pEndDate,
:pAppNo,:pGender, :pAge,:pTrainSN,:pFName,:pLName
,:pDOB
,:pAdd
,:pEmail
,:pHPhone
,:pOPhone
,:pEducation
,:pOccupation
,:pForeignYN
,:pOldYN

,:pHindi
,:pEng
,:pNep
,:pNewari
,:pLangOthers
,:pOnlineYN
--,:pNewYN
,:pCourseType
,:pFirstTrainDate
,:pFirstTrainPlace
,:pFirstTeacher
,:pLastTrainDate
,:pLastTrainPlace
,:pLastCourseTeacher
)", connection);


                    #region Parameters
                    OracleParameter pStartDate = new OracleParameter();
                    pStartDate.OracleDbType = OracleDbType.Date;
                    pStartDate.ParameterName = "pStartDate";
                    pStartDate.Value = Convert.ToDateTime(dictApplication["StartDate"]);

                    OracleParameter pEndDate = new OracleParameter();
                    pEndDate.OracleDbType = OracleDbType.Date;
                    pEndDate.ParameterName = "pEndDate";
                    pEndDate.Value = Convert.ToDateTime(dictApplication["EndDate"]);


                  

                    OracleParameter pGender = new OracleParameter();
                    pGender.OracleDbType = OracleDbType.NVarchar2;
                    pGender.ParameterName = "pGender";
                    pGender.Value = dictApplication["GEN"];

                    OracleParameter pAge = new OracleParameter();
                    pAge.OracleDbType = OracleDbType.Int32;
                    pAge.ParameterName = "pAge";
                    pAge.Value = Convert.ToInt32(dictApplication["AGE"]);

                    GetTrainSN(Convert.ToDateTime(dictApplication["StartDate"]));

                    OracleParameter pTrainSN = new OracleParameter();
                    pTrainSN.OracleDbType = OracleDbType.Varchar2;
                    pTrainSN.ParameterName = "pTrainSN";
                    pTrainSN.Value = trainSN;

                    OracleParameter pAppNo = new OracleParameter();
                    pAppNo.OracleDbType = OracleDbType.NVarchar2;
                    pAppNo.ParameterName = "pAppNo";
                    pAppNo.Value = pGender.Value.ToString() + (40 + AppNo).ToString();

                    OracleParameter pFName = new OracleParameter();
                    pFName.OracleDbType = OracleDbType.Varchar2;
                    pFName.ParameterName = "pFName";
                    pFName.Value = dictApplication["GNA"];

                    OracleParameter pLName = new OracleParameter();
                    pLName.OracleDbType = OracleDbType.NVarchar2;
                    pLName.ParameterName = "pLName";
                    pLName.Value = dictApplication["FNA"];

                    OracleParameter pDOB = new OracleParameter();
                    pDOB.OracleDbType = OracleDbType.Date;
                    pDOB.ParameterName = "pDOB";
                    pDOB.Value = Convert.ToDateTime(dictApplication["BID|BIM|BIY"]);

                    OracleParameter pAddress = new OracleParameter();
                    pAddress.OracleDbType = OracleDbType.NVarchar2;
                    pAddress.ParameterName = "pAdd";
                    pAddress.Value = string.Format("{0},{1},{2},{3},{4},{5}", dictApplication["ADD"], dictApplication["CIT"], dictApplication["PRO"], dictApplication["PCO"], dictApplication["COU"], dictApplication["NAT"]);

                    OracleParameter pEmail = new OracleParameter();
                    pEmail.OracleDbType = OracleDbType.NVarchar2;
                    pEmail.ParameterName = "pEmail";
                    pEmail.Value = dictApplication["EMA"];

                    OracleParameter pHPhone = new OracleParameter();
                    pHPhone.OracleDbType = OracleDbType.NVarchar2;
                    pHPhone.ParameterName = "pHPhone";
                    pHPhone.Value = dictApplication.ContainsKey("HPH") ? Convert.ToString(dictApplication["HPH"]) : string.Empty;

                    OracleParameter pWPhone = new OracleParameter();
                    pWPhone.OracleDbType = OracleDbType.NVarchar2;
                    pWPhone.ParameterName = "pOPhone";
                    pWPhone.Value = dictApplication.ContainsKey("MOBILEPH") ? Convert.ToString(dictApplication["MOBILEPH"]) : string.Empty;

                    OracleParameter pEducation = new OracleParameter();
                    pEducation.OracleDbType = OracleDbType.NVarchar2;
                    pEducation.ParameterName = "pEducation";
                    pEducation.Value = dictApplication["EDUCATION"];

                    OracleParameter pOccupation = new OracleParameter();
                    pOccupation.OracleDbType = OracleDbType.NVarchar2;
                    pOccupation.ParameterName = "pOccupation";
                    pOccupation.Value = dictApplication["OCCUPATION"];


                    OracleParameter pForeignYN = new OracleParameter();
                    pForeignYN.OracleDbType = OracleDbType.NVarchar2;
                    pForeignYN.ParameterName = "pForeignYN";
                    if (dictApplication["NAT"] != "NP")
                        pForeignYN.Value = "Y";
                    else
                        pForeignYN.Value = "N";

                    OracleParameter pOldYN = new OracleParameter();
                    pOldYN.OracleDbType = OracleDbType.NVarchar2;
                    pOldYN.ParameterName = "pOldYN";
                    pOldYN.Value = dictApplication["IOS"];

                    //OracleParameter pNewYN = new OracleParameter();
                    //pNewYN.OracleDbType = OracleDbType.NVarchar2;
                    //pNewYN.ParameterName = "pNewYN";
                    //pNewYN.Value = dictApplication["IOS"].ToLower() == "yes" ? "N" : "Y";


                    string primaryLanguage = dictApplication["LAN"];

                    OracleParameter pHindi = new OracleParameter();
                    pHindi.OracleDbType = OracleDbType.NVarchar2;
                    pHindi.ParameterName = "pHindi";
                    pHindi.Value = primaryLanguage == "hi" ? "Y" : "N";

                    OracleParameter pNep = new OracleParameter();
                    pNep.OracleDbType = OracleDbType.NVarchar2;
                    pNep.ParameterName = "pNep";
                    pNep.Value = primaryLanguage == "ne" ? "Y" : "N";

                    OracleParameter pEng = new OracleParameter();
                    pEng.OracleDbType = OracleDbType.NVarchar2;
                    pEng.ParameterName = "pEng";
                    pEng.Value = primaryLanguage == "en" ? "Y" : "N";


                    string otherLang = dictApplication.ContainsKey("OLA") ? dictApplication["OLA"] : "";
                    OracleParameter pNewari = new OracleParameter();
                    pNewari.OracleDbType = OracleDbType.NVarchar2;
                    pNewari.ParameterName = "pNewari";
                    pNewari.Value = otherLang.Contains("Newar") ? "Y" : "N";

                    OracleParameter pLangOthers = new OracleParameter();
                    pLangOthers.OracleDbType = OracleDbType.NVarchar2;
                    pLangOthers.ParameterName = "pLangOthers";
                    pLangOthers.Value = otherLang;

                    OracleParameter pOnlineYN = new OracleParameter();
                    pOnlineYN.OracleDbType = OracleDbType.NVarchar2;
                    pOnlineYN.ParameterName = "pOnlineYN";
                    pOnlineYN.Value = "Y";

                    OracleParameter pCourseType = new OracleParameter();
                    pCourseType.OracleDbType = OracleDbType.NVarchar2;
                    pCourseType.ParameterName = "pCourseType";
                    pCourseType.Value = dictApplication["EventType"];

                    OracleParameter pFirstTrainDate = new OracleParameter();
                    pFirstTrainDate.OracleDbType = OracleDbType.NVarchar2;
                    pFirstTrainDate.ParameterName = "pFirstTrainDate";
                    pFirstTrainDate.Value = dictApplication["FCD"];

                    OracleParameter pFirstTrainPlace = new OracleParameter();
                    pFirstTrainPlace.OracleDbType = OracleDbType.NVarchar2;
                    pFirstTrainPlace.ParameterName = "pFirstTrainPlace";
                    pFirstTrainPlace.Value = dictApplication["FCL"];

                    
                    OracleParameter pFirstCourseTeacher = new OracleParameter();
                    pFirstCourseTeacher.OracleDbType = OracleDbType.NVarchar2;
                    pFirstCourseTeacher.ParameterName = "pFirstCourseTeacher";
                    pFirstCourseTeacher.Value = dictApplication["FCT"];

                    OracleParameter pLastTrainDate = new OracleParameter();
                    pLastTrainDate.OracleDbType = OracleDbType.NVarchar2;
                    pLastTrainDate.ParameterName = "pLastTrainDate";
                    pLastTrainDate.Value = dictApplication["LCD"];

                    OracleParameter pLastTrainPlace = new OracleParameter();
                    pLastTrainPlace.OracleDbType = OracleDbType.NVarchar2;
                    pLastTrainPlace.ParameterName = "pLastTrainPlace";
                    pLastTrainPlace.Value = dictApplication["LCL"];


                    OracleParameter pLastCourseTeacher = new OracleParameter();
                    pLastCourseTeacher.OracleDbType = OracleDbType.NVarchar2;
                    pLastCourseTeacher.ParameterName = "pLastCourseTeacher";
                    pLastCourseTeacher.Value = dictApplication["LCT"];




                    command.Parameters.Add(pStartDate);
                    command.Parameters.Add(pEndDate);
                    command.Parameters.Add(pAppNo);
                    command.Parameters.Add(pGender);
                    command.Parameters.Add(pAge);
                    command.Parameters.Add(pTrainSN);
                    command.Parameters.Add(pFName);
                    command.Parameters.Add(pLName);
                    command.Parameters.Add(pDOB);
                    command.Parameters.Add(pAddress);
                    command.Parameters.Add(pEmail);
                    command.Parameters.Add(pHPhone);
                    command.Parameters.Add(pWPhone);
                    command.Parameters.Add(pEducation);
                    command.Parameters.Add(pOccupation);
                    command.Parameters.Add(pForeignYN);
                    command.Parameters.Add(pOldYN);
                  // command.Parameters.Add(pNewYN); // datatype or size problem ...need to fix later
                    command.Parameters.Add(pNep);
                    command.Parameters.Add(pHindi);
                    command.Parameters.Add(pNewari);
                    command.Parameters.Add(pEng);
                    command.Parameters.Add(pLangOthers);
                    command.Parameters.Add(pOnlineYN);
                    command.Parameters.Add(pCourseType);
                    command.Parameters.Add(pFirstTrainDate);
                    #endregion Parameters

                    int insert = command.ExecuteNonQuery();
                    command.Parameters.Clear();
                    command.Dispose();
                    //while (reader.Read())
                    //{
                    //    string myField = (string)reader["TNAME"];
                    //    HttpContext.Current.Response.Write(myField);
                    //}
                }
            }
        }

        private void GetTrainSN(DateTime dateTime)
        {
           // string trainSN = string.Empty;
            OracleDataReader rdr;
              string connectionString = GetConnectionString();
              using (OracleConnection connection = new OracleConnection())
              {
                  connection.ConnectionString = connectionString;
                  connection.Open();
                  OracleCommand command = connection.CreateCommand();
                  command = new OracleCommand("SELECT DISTINCT T_COURSE_START.TRAIN_S_N,(SELECT COUNT(1) FROM T_APPLICATION WHERE ONLINEAPPLICATION_Y_N='Y' AND Train_S_N=T_COURSE_START.TRAIN_S_N) AS AppNO FROM VIPA.T_COURSE_START  WHERE START_DATE= :pStartDate", connection);

                  OracleParameter pStartDate = new OracleParameter();
                  pStartDate.OracleDbType = OracleDbType.Date;
                  pStartDate.ParameterName = "pStartDate";
                  pStartDate.Value = dateTime;

                  command.Parameters.Add(pStartDate);
                  rdr = command.ExecuteReader();
                  while (rdr.Read())
                  {
                      trainSN = rdr["TRAIN_S_N"].ToString();
                      AppNo = Convert.ToInt32(rdr["APPNO"]);
                  }

              }

             
        }



        protected void GetApplicationsFromGmail()
        {
            string xmlString = string.Empty;
            MailRepository rep = new MailRepository("imap.gmail.com", 993, true, @"dhammashringaxml@gmail.com", @"dhamma@xml");
            foreach (Message email in rep.GetUnreadMails("Applications"))
            {
                // Response.Write(string.Format("<p>{0}: {1}</p><p>{2}</p>", email.From, email.Subject, email.BodyHtml.Text));
                if (email.From.Email == "pforms-application@registrar.dhamma.org")
                {
                    if (email.Attachments.Count > 0)
                    {
                        foreach (MimePart attachment in email.Attachments)
                        {
                            //attachment.
                            //  attachment.TextContent
                            xmlString = attachment.TextContent;
                            ReadXmlApplication(xmlString);

                        }
                    }
                }
            }

            InsertApplication();
        }

        List<Dictionary<string, string>> lstApplications = new List<Dictionary<string, string>>();
        protected void ReadXmlApplication(string xmlApplication)
        {
            // xmlApplication = xmlApplication.re
            Dictionary<string, string> dictApplication = new Dictionary<string, string>();
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(xmlApplication);

            XmlNode evntList = doc.GetElementsByTagName("Event")[0] as XmlNode;
            XmlNodeList elemList = doc.GetElementsByTagName("AppItem");

            foreach (XmlNode node in evntList.ChildNodes)
            {
                if (node.Name == "StartDate" || node.Name == "EndDate" || node.Name == "EventType")
                {
                    dictApplication.Add(node.Name, node.InnerText);
                }
            }

            foreach (XmlNode node in elemList)
            {              
                if (!dictApplication.Keys.Contains(node.FirstChild.LastChild.InnerText))
                    dictApplication.Add(node.FirstChild.LastChild.InnerText, node.ChildNodes[2].LastChild.InnerText);
            }

            lstApplications.Add(dictApplication);
        }



        protected void Import_Click(object sender, EventArgs e)
        {

            GetApplicationsFromGmail();
            ShowApplications();
            //XmlDataSource application = new XmlDataSource();
            //application.DataFile = @"~/Application.xml";
            //// application.
            //application.DataBind();
            //gvApplication.DataSource = application;
            //gvApplication.DataBind();



            //string filePath = Server.MapPath("application.xml");
            //System.IO.StreamReader file = new System.IO.StreamReader(filePath);
            //String URLString = "~/application.xml";

            //XmlDocument doc = new XmlDocument();
            ////doc.LoadXml();
            //doc.Load(file);
            //XmlNode evntList = doc.GetElementsByTagName("Event")[0] as XmlNode;
            //XmlNodeList elemList = doc.GetElementsByTagName("AppItem");

            //foreach (XmlNode node in evntList.ChildNodes)
            //{
            //    if (node.Name == "StartDate" || node.Name == "EndDate")
            //    {
            //        dictApplication.Add(node.Name, node.InnerText);
            //    }
            //}
            //// for (int i = 0; i < elemList.Count; i++)
            //foreach (XmlNode node in elemList)
            //{
            //    if (!dictApplication.Keys.Contains(node.ChildNodes[1].LastChild.InnerText))
            //        dictApplication.Add(node.ChildNodes[1].LastChild.InnerText, node.ChildNodes[2].LastChild.InnerText);
            //    // Response.Write(node.ChildNodes[1].LastChild.InnerText + " = " + node.ChildNodes[2].LastChild.InnerText + "<br/>");
            //    //foreach (XmlNode childNodes in node.ChildNodes)
            //    //{

            //    //    if (childNodes.Name != "AppItemKey")
            //    //    { 
            //    //        Response.Write(childNodes.LastChild.InnerText + " || ");
            //    //    }
            //    //}
            //}
            // ConnectingToOracle();
            // Response.Write(application["Select Gender"]);
            // XmlElement root = doc.DocumentElement;

            //// List<Book> books = new List<Book>();

            // Response.Write(root["Event"]["StartDate"].InnerText);

            //XmlTextReader reader = new XmlTextReader(file);

            //while (reader.Read())
            //{
            //    switch (reader.NodeType)
            //    {
            //        case XmlNodeType.Element: // The node is an element.
            //            {
            //                if (reader.Name == "StartDate" || reader.Name == "EndDate")
            //                    Response.Write(reader.ReadInnerXml() + "--");
            //                else if (reader.Name == "EnglishPromptText")
            //                {
            //                    reader.ReadToDescendant("OptionValue");
            //                    Response.Write(reader.ReadInnerXml() + "--");
            //                }
            //                //    while (reader.MoveToNextAttribute()) // Read the attributes.
            //                //        Response.Write(" " + reader.Name + "='" + reader.Value + "'");
            //                //    Response.Write(">");
            //            }
            //            break;
            //        //case XmlNodeType.Text: //Display the text in each element.
            //        //    Response.Write(reader.Value);
            //        //    break;
            //        //case XmlNodeType.EndElement: //Display the end of the element.
            //        //    Response.Write("</" + reader.Name);
            //        //    Response.Write(">");
            //        //    break;
            //    }
            //}
        }

    }

    public class Application
    {
        public DateTime startDate;
        public DateTime endDate;

    }
}