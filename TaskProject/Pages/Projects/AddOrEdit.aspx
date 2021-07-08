<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AddOrEdit.aspx.cs" Inherits="TaskProject.Pages.ProjectManager.AddOrEdit" %>


<asp:Content runat="server" ID="cntnt" ContentPlaceHolderID="MainContent">
    <link href="../../Content/Project-AddOrEdit.css" rel="stylesheet" />
    <div style="display: flex; justify-content: center;">
        <div class="create-form">
            <div class="form-group">
                <asp:Label runat="server" ID="TitleLabel" style="font-size:53px;">New Project</asp:Label>
            </div>
            <div>
                <asp:TextBox hidden="true" runat="server" ID="PId" Text=""></asp:TextBox>
            </div>
            <div class="form-group">
                <asp:Label ID="validationMessages" CssClass="text-danger" runat="server"></asp:Label>
            </div>
            <div style="min-width: 350px;">
                <div class="form-group">
                    <label>Name *</label>
                    <asp:TextBox  runat="server" ID="ProjectName" CssClass="form-control"></asp:TextBox> 
                    <asp:RequiredFieldValidator ErrorMessage="Pleas Enter a Project Name" CssClass="text-danger" ControlToValidate="ProjectName" runat="server" />
                </div>
                <div class="form-group">
                    <label>Description</label>
                    <asp:TextBox runat="server" ID="PDescription" CssClass="form-control"></asp:TextBox>

                </div>
                <div class="form-group">
                    <label>Start Time *</label>
                    <asp:TextBox runat="server" ID="StartDate" type="datetime-local" class="form-control" onkeypress="return false" />
                    <asp:RequiredFieldValidator ErrorMessage="This Field Is Required" CssClass="text-danger" ControlToValidate="StartDate" runat="server" />
                </div>
                <div class="form-group">
                    <label>End Time *</label>
                    <asp:TextBox type="datetime-local" runat="server" ID="EndDate" class="form-control" onkeypress="return false" />
                    <asp:RequiredFieldValidator ErrorMessage="This Field Is Required" CssClass="text-danger" ControlToValidate="EndDate" runat="server" />
                </div>
                <div class="form-group">
                    <label>Budget *</label>
                    <asp:TextBox runat="server" ID="PBudget" type="number" Text="0" CssClass="form-control"></asp:TextBox>
                    <asp:RequiredFieldValidator ErrorMessage="Budget Is Required" CssClass="text-danger" ControlToValidate="PBudget" runat="server" />
                </div>
                <div class="form-group">
                    <label>Company Name *</label>
                    <asp:TextBox runat="server" ID="PCompanyName" CssClass="form-control"></asp:TextBox>
                    <asp:RequiredFieldValidator ErrorMessage="Company Name Is Required" CssClass="text-danger" ControlToValidate="PCompanyName" runat="server" />
                </div>
                <div class="form-group">
                    <label>Company Location</label>
                    <asp:TextBox runat="server" ID="PCompanyLocation" CssClass="form-control"></asp:TextBox>
                </div>
                <div class="form-group">
                    <label>Company Phone</label>
                    <asp:TextBox runat="server" ID="PCompanyPhone" CssClass="form-control"></asp:TextBox>
                    <asp:RegularExpressionValidator CssClass="text-danger" ErrorMessage="Enter a valid phone number" ControlToValidate="PCompanyPhone" runat="server"
                        ValidationExpression="^[+]*[(]{0,1}[0-9]{1,4}[)]{0,1}[-\s\./0-9]*$"/>
                </div>
                <div class="form-group">
                    <label>Company Email</label>
                    <asp:TextBox runat="server" type="email" ID="PCompanyEmail" CssClass="form-control"></asp:TextBox>
                </div>
                <div class="form-group">
                    <label>Company Website</label>
                    <asp:TextBox runat="server" ID="PCompanyWebsite" CssClass="form-control"></asp:TextBox>
                    <asp:RegularExpressionValidator ErrorMessage="Enter a valid website" CssClass="text-danger" ControlToValidate="PCompanyWebsite" runat="server"
                        ValidationExpression="(https?:\/\/)?(www\.)[-a-zA-Z0-9@:%._\+~#=]{2,256}\.[a-z]{2,4}\b([-a-zA-Z0-9@:%_\+.~#?&//=]*)|(https?:\/\/)?(www\.)?(?!ww)[-a-zA-Z0-9@:%._\+~#=]{2,256}\.[a-z]{2,4}\b([-a-zA-Z0-9@:%_\+.~#?&//=]*)"/>
                </div>
                <div class="form-group">
                    <label>Manager *</label>
                    <asp:DropDownList CssClass="form-control" runat="server" ID="UsersList">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ErrorMessage="Please Choose a Manager" CssClass="text-danger" ControlToValidate="UsersList" runat="server" />
                </div>
                <div class="form-group">
                    <label>Files</label>
                    <asp:FileUpload ID="files" runat="server" onchange="validateFileExtension(this);" CssClass="form-control" AllowMultiple="true"/>
                </div>
                <div class="form-group">
                    <label>Images</label>
                    <asp:FileUpload ID="images" runat="server" onchange="validateFileExtension(this);" CssClass="form-control" AllowMultiple="true"/>
                    <asp:RegularExpressionValidator ID="regExVal" runat="server" CssClass="text-danger"
                        ErrorMessage="Please upload your image in .jpg, .png, or .gif."
                        ControlToValidate="images"
                        ValidationExpression="(.*?)\.(jpg|jpeg|png|gif|JPG|JPEG|PNG|GIF)$" />
                </div>
                <div class="form-group">
                    <label>Project Website</label>
                    <asp:TextBox runat="server" ID="PWebsite" CssClass="form-control"></asp:TextBox>
                    <asp:RegularExpressionValidator ErrorMessage="Enter a valid website" CssClass="text-danger" ControlToValidate="PWebsite" runat="server"
                        ValidationExpression="(https?:\/\/)?(www\.)[-a-zA-Z0-9@:%._\+~#=]{2,256}\.[a-z]{2,4}\b([-a-zA-Z0-9@:%_\+.~#?&//=]*)|(https?:\/\/)?(www\.)?(?!ww)[-a-zA-Z0-9@:%._\+~#=]{2,256}\.[a-z]{2,4}\b([-a-zA-Z0-9@:%_\+.~#?&//=]*)"/>
                </div>
                <div>
                    <br />
                    <input type="submit" style="border:black solid; width:150px" class="btn" value="Confirm" />
                </div>
            </div>
        </div>
    </div>
</asp:Content>
