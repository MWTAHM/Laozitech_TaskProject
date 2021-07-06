<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AddOrEdit.aspx.cs" Inherits="TaskProject.Pages.Task.AddOrEdit" %>


<asp:Content runat="server" ID="cntnt" ContentPlaceHolderID="MainContent">

    <style>

        .create-form {
            min-width: 300px;
            width:550px;
            border: black solid;
            margin: 30px;
            padding: 50px;
            border-radius: 10px;
            background-color: rgba(169,134,94,0.8);
            text-align: center;
        }

        .form-control {
            max-width: 100%;
        }

        body{
            background-image:url(../../Images/work.jpg);
            background-attachment:fixed;
            background-repeat:no-repeat;
            background-size:cover;
            backdrop-filter:blur(5px);
        }
    </style>
    <div style="display: flex; justify-content: center;">
        <div class="create-form">
            <div class="form-group">
                <asp:Label runat="server" ID="TitleLabel" style="font-size:53px;">New Task</asp:Label>
            </div>
            <div>
                <asp:TextBox hidden="true" runat="server" ID="TaskId" Text=""></asp:TextBox>
                <asp:TextBox hidden="true" runat="server" ID="ParentId" Text=""></asp:TextBox>
            </div>
            <div class="form-group">
                <asp:Label ID="validationMessages" CssClass="text-danger" runat="server"></asp:Label>
            </div>
            <div style="min-width: 350px;">
                <div class="form-group">
                    <label>Name *</label>
                    <asp:TextBox  runat="server" ID="TaskName" CssClass="form-control"></asp:TextBox> 
                    <asp:RequiredFieldValidator ErrorMessage="Pleas Enter a Project Name" CssClass="text-danger" ControlToValidate="TaskName" runat="server" />
                </div>
                <div class="form-group">
                    <label>Description</label>
                    <asp:TextBox runat="server" ID="TaskDesc" CssClass="form-control"></asp:TextBox>
                </div>
                <div class="form-group">
                    <label>Start Time *</label>
                    <asp:TextBox runat="server" ID="StartDate" type="datetime-local" class="form-control" onkeypress="return false" />
                    <asp:RequiredFieldValidator ErrorMessage="This Field Is Required" ControlToValidate="StartDate" runat="server" />
                </div>
                <div class="form-group">
                    <label>End Time *</label>
                    <asp:TextBox type="datetime-local" runat="server" ID="EndDate" class="form-control" onkeypress="return false" />
                    <asp:RequiredFieldValidator ErrorMessage="This Field Is Required" ControlToValidate="EndDate" runat="server" />
                </div>
                <div class="form-group">
                    <label>Files</label>
                    <asp:FileUpload ID="files" runat="server" CssClass="form-control" AllowMultiple="true"/>
                </div>
                <div class="form-group">
                    <label>Images</label>
                    <asp:FileUpload ID="images" runat="server" onchange="validateFileExtension(this);" CssClass="form-control" AllowMultiple="true"/>
                    <asp:RegularExpressionValidator ID="regExVal" runat="server"
                        ErrorMessage="Please upload your image in .jpg, .png, or .gif."
                        ControlToValidate="images"
                        ValidationExpression="(.*?)\.(jpg|jpeg|png|gif|JPG|JPEG|PNG|GIF)$" />
                </div>
                <div>
                    <br />
                    <input type="submit" style="border:black solid; width:150px" class="btn" value="Confirm" />
                </div>
            </div>
        </div>
    </div>
</asp:Content>