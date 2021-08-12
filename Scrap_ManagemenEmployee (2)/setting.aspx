<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="setting.aspx.cs" Inherits="setting" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    
        <div class="row">
            <div class="col s12 m12 l12 ">
          <div class="section-title">
            <span class="theme-secondary-color">PROFILE</span>
          </div>
        </div>
        </div>
    <div>
       
    <%--    <div class="row mt-20">
          <div class="file-field input-field col s12 m12 l12 no-margin-top" >
            <div class="btnfile">
              <span>File</span>
              <input type="file" multiple></div>
            <div class="file-path-wrapper">
              <input class="file-path validate" type="text" placeholder="Upload one or more files"></div>
          </div>
        </div>--%>
        <div class="row">
          <div class="input-field col s12 m12 l12 ">
             <asp:TextBox ID="Txtfirstname" class="validate" runat="server"></asp:TextBox>
            <label for="user-firstname" id="First" runat="server">First Name</label>
          </div>
        </div>
       <%-- <div class="row">
          <div class="input-field col s12 m12 l12 ">
             <asp:TextBox ID="TxtLastName" class="validate" runat="server"></asp:TextBox>
            <label for="user-lastname" id="Last" runat="server">Last Name</label>
          </div>
        </div>--%>
        <div class="row">
          <div class="input-field col s12 m12 l12 ">
          <asp:TextBox ID="TxtEmail" class="validate" runat="server"></asp:TextBox>
            <label for="user-email" id="Email" runat="server">Email</label>
          </div>
        </div>
        <div class="row">
          <div class="input-field col s12 m12 l12 ">
          <asp:TextBox ID="TxtPhone" class="validate" runat="server"></asp:TextBox>
            <label for="user-phone" id="Phone" runat="server">Phone</label>
          </div>
        </div>
        <%--<div class="row">
          <div class="input-field col s12 m12 l12 ">
            <textarea id="user-address" class="materialize-textarea">Wijilan St, Yogyakarta, Indonesia</textarea>
            <label for="user-address" id="Address" runat="server">Address</label>
          </div>
          <div class="row">
            <div class="input-field col s12 m6 l4 offset-m3 offset-l4 center">
              <input class="waves-effect waves-light btn" value="UPDATE" type="submit"></div>
          </div>
        </div>--%>
      </div>
       
</asp:Content>

