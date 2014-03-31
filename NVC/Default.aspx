<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="NVC._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
     <% //Initilize thr Progress Bar and show a message
        initNotify("Welcome to NVC Registration Application!");
        System.Threading.Thread.Sleep(2000);
    %>
    <div class="jumbotron">
        <h1>NVC REGISTRATION</h1>
        <p class="lead">NVC Registration is a tool for managing new and old students applications.</p>
        <p><a href="http://www.asp.net" class="btn btn-primary btn-large">Learn more &raquo;</a></p>
    </div>

    <div class="row">
         <% 
            // We have achieved a milestone. Let the user know!
            Notify("30", "Loading Headers Completed ...");
            // Simulate Internet delay
           System.Threading.Thread.Sleep(1000);
           
             %>
       <%--  <div class="col-md-4" style="background:#0cd426">
            <h2>New Applications</h2>
            <p>
                You have  new Application(s)
            </p>
            <p>
                <a class="btn btn-default" href="ImportApplications.aspx">Import Applications &raquo;</a>
            </p>
        </div>--%>
       
    <%--    <div class="col-md-4" style="background:#ffd800">
            <h2>Upcoming Course details</h2>
            <p>
                Course No:1000000
               Seats Available:1000
               Seats Remaining: 100 
            </p>
            <p>
                <a class="btn btn-default" href="http://go.microsoft.com/fwlink/?LinkId=301948">Learn more &raquo;</a>
            </p>
        </div>
       
        <div class="col-md-4">
            <h2>Teachers Profile</h2>
            <p>
                You can easily find teachers information here.
                Total no of teachers 100.
            </p>
            <p>
                <a class="btn btn-default" href="http://go.microsoft.com/fwlink/?LinkId=301950">Learn more &raquo;</a>
            </p>
        </div>--%>

        <div class="widget-body">
                    <div class="row-fluid">
                      <div class="metro-nav">
                    <%--    <div class="metro-nav-block nav-block-orange">
                          <a href="#" data-original-title="">
                            <div class="fs1" aria-hidden="true" data-icon=""></div>
                            <div class="info">692</div>
                            <div class="brand">Clients</div>
                          </a>
                        </div>--%>
                        <div class="metro-nav-block nav-block-green">
                          <a href="#" data-original-title="">
                            <div class="fs1" aria-hidden="true" data-icon=""></div>
                            <div class="info"><asp:Label ID="lblNo" runat="server" Font-Bold="true" Font-Underline="true">0</asp:Label></div>
                            <div class="brand">New Applications</div>
                          </a>
                        </div>
                           <% 
            // We have achieved a milestone. Let the user know!
            Notify("100", "Loading Apllication Count Completed ...");
            // Simulate Internet dalay
           System.Threading.Thread.Sleep(1000);
           
             %>
                        <div class="metro-nav-block nav-block-blue double">
                          <a href="#" data-original-title="">
                            <div class="fs1" aria-hidden="true" data-icon=""></div>
                            <div class="info">1000</div>
                            <div class="brand">Upcoming Course</div>
                          </a>
                        </div>
                        <div class="metro-nav-block nav-block-yellow">
                          <a href="#" data-original-title="">
                              <div class="fs1" aria-hidden="true" data-icon=""></div>
                              <div class="info">100</div>
                            <div class="brand">Teachers</div>
                          </a>
                        </div>
                        <%--<div class="metro-nav-block nav-block-red">
                          <a href="#" data-original-title="">
                            <div class="fs1" aria-hidden="true" data-icon=""></div>
                            <div class="info">288</div>
                            <div class="brand">Cancelled</div>
                          </a>
                        </div>
                        <div class="metro-nav-block nav-block-yellow">
                          <a href="#" data-original-title="">
                            <div class="fs1" aria-hidden="true" data-icon=""></div>
                            <div class="info">193</div>
                            <div class="brand">Signup</div>
                          </a>
                        </div>
                        <div class="metro-nav-block nav-block-green double">
                          <a href="#" data-original-title="">
                            <div class="fs1" aria-hidden="true" data-icon=""></div>
                            <div class="info">$39432</div>
                            <div class="brand">Stock</div>
                          </a>
                        </div>
                        <div class="metro-nav-block nav-block-orange">
                          <a href="#" data-original-title="">
                            <div class="fs1" aria-hidden="true" data-icon=""></div>
                            <div class="info">434</div>
                            <div class="brand">Messages</div>
                          </a>
                        </div>
                        <div class="metro-nav-block nav-block-blue">
                          <a href="#" data-original-title="">
                            <div class="fs1" aria-hidden="true" data-icon=""></div>
                            <div class="info">985</div>
                            <div class="brand">Posts</div>
                          </a>
                        </div>
                        <div class="metro-nav-block nav-block-yellow">
                          <a href="#" data-original-title="">
                            <div class="fs1" aria-hidden="true" data-icon=""></div>
                            <div class="info">199</div>
                            <div class="brand">Tweets</div>
                          </a>
                        </div>--%>

                      </div>
                      
                    </div>
                  </div>
    </div>
      <%
            Notify("100", "Loading  Completed...");
          System.Threading.Thread.Sleep(1000);
         %>
</asp:Content>
