<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Registration.aspx.cs" Inherits="TaskProject.Pages.UserControl.WebForm2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
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
                <asp:Label runat="server" ID="TitleLabel" style="font-size:53px;">Register</asp:Label>
            </div>
            <div class="form-group">
                <asp:Label ID="validationMessages" CssClass="text-danger" runat="server"></asp:Label>
            </div>
            <div style="min-width: 350px;">
                <div class="form-group">
                    <label>UserName *</label>
                    <asp:TextBox  runat="server" ID="UName" CssClass="form-control"></asp:TextBox> 
                    <asp:RequiredFieldValidator ErrorMessage="Please Pick a UserName" CssClass="text-danger" ControlToValidate="UName" runat="server" />
                </div>
                <div class="form-group">
                    <label>Full Name *</label>
                    <asp:TextBox runat="server" ID="UFullName" CssClass="form-control"></asp:TextBox>
                    <asp:RequiredFieldValidator ErrorMessage="Please Enter a Your Name" CssClass="text-danger" ControlToValidate="UFullName" runat="server" />
                </div>
                <div class="form-group">
                    <label>Email *</label>
                    <asp:TextBox runat="server" ID="UEmail" type="email" class="form-control" />
                    <asp:RequiredFieldValidator ErrorMessage="This Email Is Required" CssClass="text-danger" ControlToValidate="UEmail" runat="server" />
                </div>
                <div class="form-group">
                    <label>BirthDate</label>
                    <asp:TextBox type="date" runat="server" ID="BirthDate" CssClass="form-control" onkeypress="return false" />  
                </div>
                <div class="form-group">
                    <label>Password *</label>
                    <asp:TextBox runat="server" ID="Password" CssClass="form-control" type="password"></asp:TextBox>
                    <asp:RegularExpressionValidator ErrorMessage="Enter a strong password" CssClass="text-danger" ControlToValidate="Password" runat="server"
                        ValidationExpression="^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d]{8,}$" />
                </div>
                <div class="form-group">
                    <label>Confirm Password *</label>
                    <asp:TextBox runat="server" ID="ConfirmPassword" CssClass="form-control" type="password"></asp:TextBox>                    
                    <asp:CompareValidator ErrorMessage="Password and Confirm Password Do Not Match" CssClass="text-danger" ControlToCompare="Password" ControlToValidate="ConfirmPassword"
                        runat="server"></asp:CompareValidator>
                </div>
                <div class="form-group">
                    <label>Phone Number *</label>
                    <asp:TextBox runat="server" ID="UPhone" CssClass="form-control"></asp:TextBox>
                    <asp:RegularExpressionValidator CssClass="text-danger" ErrorMessage="Enter a valid phone number" ControlToValidate="UPhone" runat="server"
                        ValidationExpression="^[+]*[(]{0,1}[0-9]{1,4}[)]{0,1}[-\s\./0-9]*$"/>
                </div>
                <div class="form-group">
                    <label>Country *</label>
                    <asp:TextBox runat="server" ID="UCountry" CssClass="form-control"></asp:TextBox>
                    <asp:RequiredFieldValidator ErrorMessage="Please Enter Youe Country" CssClass="text-danger" ControlToValidate="UCountry" runat="server" />
                </div>
                <div class="form-group">
                    <label>City *</label>
                    <asp:TextBox runat="server" ID="UCity" CssClass="form-control"></asp:TextBox>
                    <asp:RequiredFieldValidator ErrorMessage="Please Enter Youe City" CssClass="text-danger" ControlToValidate="UCity" runat="server" />
                </div>
                <div class="form-group">
                    <label>Address Line 1 *</label>
                    <asp:TextBox runat="server" ID="UAddress1" CssClass="form-control"></asp:TextBox>
                    <asp:RequiredFieldValidator ErrorMessage="Please Enter Youe Address" CssClass="text-danger" ControlToValidate="UAddress1" runat="server" />
                </div>
                <div class="form-group">
                    <label>Address Line 2</label>
                    <asp:TextBox runat="server" ID="UAddress2" CssClass="form-control"></asp:TextBox>
                </div>
                <div class="form-group">
                    <label>Postal Code *</label>
                    <asp:TextBox runat="server" ID="UPostal" CssClass="form-control"></asp:TextBox>
                    <asp:RequiredFieldValidator ErrorMessage="Please Enter Postal Code" CssClass="text-danger" ControlToValidate="UPostal" runat="server" />
                </div>
                <div>
                    <br />
                    <input type="submit" style="border:black solid; width:150px" class="btn" value="Confirm" />
                </div>
            </div>
        </div>
    </div>
</asp:Content>
