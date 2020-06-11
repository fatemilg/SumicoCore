<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="SCMCore.Admin.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">

    <title>ورود به پنل مدیریت</title>
    <meta charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1, maximum-scale=1, user-scalable=0" />
    <!--320-->

    <!-- <link rel="shortcut icon" href="images/favicon.ico" type="image/x-icon">
    <link rel="icon" href="images/favicon.ico" type="image/x-icon"> -->
    <!-- Bootstrap CSS -->
    <link href="bs3/css/bootstrap.min.css" rel="stylesheet" />

    <link rel="stylesheet" href="css/login/material.css" />

    <link rel="stylesheet" type="text/css" href="css/login/signin.css" />
    <!-- custom scrollbar stylesheet -->
    <link rel="stylesheet" type="text/css" href="css/login/style.css" />
</head>
<body>
    <form id="form1" runat="server">
        <asp:Panel runat="server">
            <div class="signin_wrapper">
                <div class="row" style="font-family: BRoyas; direction: rtl; text-align: right">
                    <div class="right_block">
                        <div class="row">
                            <div class="col-md-12 col-sm-12 col-xs-12">
                                <h2 class="signup-heading">Sign in to the SCMCore</h2>
                                <div class="row">
                                    <div class="input-field col-md-12 col-sm-12 col-xs-12">
                                        <i class="ion-coffee prefix"></i>
                                        <asp:TextBox ID="txtUserName" CssClass="validate" runat="server" autocomplete="off" Style="direction: ltr" TabIndex="1" data-ng-model="UserName"></asp:TextBox>
                                        <label for="icon_prefix-2">Username</label>
                                    </div>
                                    <div class="input-field col-md-12 col-sm-12 col-xs-12">
                                        <i class="ion-key prefix"></i>
                                        <asp:TextBox runat="server" TextMode="Password" ID="txtPassword" CssClass="validate" Style="direction: ltr" TabIndex="2" data-ng-model="Password"></asp:TextBox>
                                        <label for="icon_prefix-2">Password</label>
                                    </div>
                                </div>
                                <asp:Button ID="btLogin" CssClass="btn btn-primary btn-block" Style="width: 90%;" UseSubmitBehavior="false" runat="server" Text="Sign in" TabIndex="3" OnClick="btLogin_Click"  />
                                <asp:Label ID="lblMessage" ForeColor="White" runat="server" Style="font-size: 17px; font-weight: bold" Text=""></asp:Label>
                            </div>
                        </div>
                    </div>
                    <!-- right_block -->
                </div>
                <!-- row -->



            </div>
        </asp:Panel>



        <!-- jQuery -->
        <script src="js/jquery.js"></script>
        <!-- Bootstrap JavaScript -->
<%--        <script src="js/bootstrap.min.js"></script>--%>
        <!-- custom scrollbar plugin -->
        <!-- Compiled and minified JavaScript -->
        <script src="js/materialize.js"></script>
    </form>
</body>
</html>
