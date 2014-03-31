<%@ Page Title="Manage Applications" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ImportApplications.aspx.cs" Inherits="NVC.ImportApplications" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
  
  
     <div class="form-horizontal" style="margin-right:5px">
          
     <div class="form-group"> <h2><%: Title %>.
</h2>
          <div style="float:right;padding-left:15px;clear:both">
                <asp:Button runat="server" ID="Import" Text="Import Applications" CssClass="btn btn-primary btn-large" OnClick="Import_Click" />
        </div>
         <div style="clear:both"></div>
         <div style="clear:both">
              <h3>Recent applcations.</h3>
             <asp:GridView runat="server" ID="gvApplication"  GridLines="None"
           AutoGenerateColumns="true"
            CssClass="table table-striped table-hover"
            PagerStyle-CssClass="pgr"
            AlternatingRowStyle-CssClass="alt"></asp:GridView>

         </div>
           
        </div>
        </div>
   <%-- <address>
        One Microsoft Way<br />
        Redmond, WA 98052-6399<br />
        <abbr title="Phone">P:</abbr>
        425.555.0100
    </address>

    <address>
        <strong>Support:</strong>   <a href="mailto:Support@example.com">Support@example.com</a><br />
        <strong>Marketing:</strong> <a href="mailto:Marketing@example.com">Marketing@example.com</a>
    </address>--%>
   
</asp:Content>
