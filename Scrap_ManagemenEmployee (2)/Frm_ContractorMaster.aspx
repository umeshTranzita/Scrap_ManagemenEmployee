<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Frm_ContractorMaster.aspx.cs" Inherits="Frm_ContractorMaster" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div class="row mt-20">

        <div class="shipping-checkout-page">
            <div class="row">
                <div class="col s12">
                    <div class="section-title">
                        <p><span style="color: #101010">CONTRACTOR</span> MASTER</p>
                    </div>
                </div>
            </div>
            <div class="billing-detail-wrap ck-box mt-20">

                <div class="row">
                    <div class="col s12">
                        <%-- <div class="billing-deatail-form-text"><i class="far fa-id-card"></i>Fill in the form your Product detail :</div>--%>
                    </div>
                </div>
                <div class="row">
                    <div class="col s12">
                        <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                        <asp:Label ID="lblUserID" runat="server" class="form-control" Visible="false"></asp:Label>
                        <asp:Label ID="lblRoleID" runat="server" class="form-control" Visible="false"></asp:Label>
                        <asp:Label ID="lblEmail_ID" runat="server" class="form-control" Visible="false"></asp:Label>
                    </div>
                </div>

                <div class="row" style="padding-top: 20px;">
                    <div class="input-field col s6 m12 l6">
                        <div class="col s12 m12 l6">
                            <asp:DropDownList runat="server" ID="ddlSite" AutoPostBack="True" class="form-control select2" OnSelectedIndexChanged="ddlSite_SelectedIndexChanged"></asp:DropDownList>
                            <%--    <label>Select Category<span style="color: red;">*</span></label>--%>
                        </div>
                    </div>
                </div>
                <div class="row" style="padding-top: 20px;">


                    <div class="input-field col s6 m12 l6">
                        <asp:TextBox ID="txtContractorName" class="validate" runat="server" MaxLength="50" AutoComplete="off"></asp:TextBox>
                        <label id="ContractorName" runat="server">Enter Contractor Name<span style="color: red;">*</span></label>
                    </div>
                    <div class="input-field col s6 m12 l6 ">
                        <asp:TextBox ID="txtContractorCode" class="validate" runat="server" AutoComplete="off"></asp:TextBox>
                        <label id="ContractorCode" runat="server">Enter Contractor Code<span style="color: red;">*</span></label>
                    </div>
                    <div class="input-field col s6 m12 l6 hidden">
                        <asp:TextBox ID="TxtContractorMobNo" class="validate" runat="server" onkeypress="return validateNum(event)" AutoComplete="off"></asp:TextBox>
                        <label id="ContractorMob" runat="server">Enter Contractor ContactNo<span style="color: red;">*</span></label>
                    </div>
                    <div class="input-field col s6 m12 l6 ">
                        <asp:TextBox ID="TxtVendorName" class="validate" runat="server" AutoComplete="off"></asp:TextBox>
                        <label id="VendorName" runat="server">Enter Vendor Name<span style="color: red;">*</span></label>
                    </div>
                    <div class="input-field col s6 m12 l6 hidden">
                        <asp:TextBox ID="TxtVendorContactNo" class="validate" runat="server" onkeypress="return validateNum(event)" AutoComplete="off"></asp:TextBox>
                        <label id="VendorMob" runat="server">Enter Vendor ContactNo<span style="color: red;">*</span></label>
                    </div>
                    <div class="input-field col s6 m12 l6">
                        <div class="col s12 m12 l6">
                            <asp:DropDownList runat="server" ID="ddlRole" AutoPostBack="false" class="form-control select2"></asp:DropDownList>
                            <%--    <label>Select Category<span style="color: red;">*</span></label>--%>
                        </div>
                    </div>
                </div>


                <div class="col s6 m12 l6 ">
                    <input type="checkbox" class="filled-in" id="chkIsActive" checked="checked" clientidmode="Static" runat="server" />
                    <%--<asp:CheckBox ID="chkIsActive" CssClass="filled-in" runat="server" Checked="true" />--%>
                    <label class="label-checkbox" for="chkIsActive">Active Status</label>
                </div>
                <div class="row">
                    <%-- OnClientClick="return validateform();"--%>
                    <div class="input-field col s12 m12 l12">
                        <asp:Button ID="btnSubmit" runat="server" class="btn btn-sm" Text="SUBMIT" OnClick="btnSubmit_Click" OnClientClick="validateform()" />
                        <asp:Button ID="btnCancel" runat="server" class="btn btn-cancel btn-sm" Text="CANCEL" />
                    </div>
                </div>
            </div>
            <div class="mt-20">
                <div class="table-responsive">

                    <asp:GridView ID="GridView1" runat="server" class="table table-bordered table-striped" AutoGenerateColumns="False"
                        AllowPaging="True" DataKeyNames="Id" PageSize="100" OnSelectedIndexChanged="GridView1_SelectedIndexChanged" OnPageIndexChanged="GridView1_PageIndexChanged" OnPageIndexChanging="GridView1_PageIndexChanging">
                        <Columns>
                            <asp:TemplateField HeaderText="SNo." ItemStyle-Width="10" ItemStyle-CssClass="text-center">
                                <ItemTemplate>
                                    <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" ToolTip='<%#Bind("Id") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:BoundField DataField="Contractor_Name" HeaderText="Contractor Name" />
                            <asp:BoundField DataField="Contractor_Code" HeaderText="Contractor Code" />

                            <asp:BoundField DataField="Vendor_Name" HeaderText="Vendor Name" />

                            <asp:BoundField DataField="Role_Name" HeaderText="Role" />
                            <asp:BoundField DataField="IsActive" HeaderText="IsActive" />

                            <asp:TemplateField HeaderText="Edit" ShowHeader="False" ItemStyle-Width="60">
                                <ItemTemplate>
                                    <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" CommandName="Select" Text="EDIT" CssClass="btn btn-block btn-info btn-xs"></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>

                        </Columns>
                    </asp:GridView>
                </div>
            </div>



        </div>



        <script type="text/javascript">


            function validateNum(evt) {
                evt = (evt) ? evt : window.event;
                var charCode = (evt.which) ? evt.which : evt.keyCode;
                if (charCode > 31 && (charCode < 48 || charCode > 57)) {
                    return false;
                }
                return true;

            }

            function validateform() {
                var msg = "";
                if (document.getElementById('<%=ddlRole.ClientID%>').selectedIndex == 0) {
                    msg += "Please Select Role.\n";
                }
                if (document.getElementById('<%=txtContractorName.ClientID%>').value.trim() == "") {
                    msg += "Enter Contractor Name. \n";
                }
                if (document.getElementById('<%=txtContractorCode.ClientID%>').value.trim() == "") {
                    msg += "Enter Contractor Code. \n";
                }
               <%-- if (document.getElementById('<%=TxtContractorMobNo.ClientID%>').value.trim() == "") {
                    msg += "Enter Contractor Contact No. \n";
                }--%>
                if (document.getElementById('<%=TxtVendorName.ClientID%>').value.trim() == "") {
                    msg += "Enter Vendor Name. \n";
                }
               <%-- if (document.getElementById('<%=TxtVendorContactNo.ClientID%>').value.trim() == "") {
                    msg += "Enter Vendor Contact No. \n";
                }--%>
                if (msg != "") {
                    alert(msg);
                    return false;
                }
                else {
                    if (document.getElementById('<%=btnSubmit.ClientID%>').value.trim() == "SUBMIT") {
                        if (confirm("Do you really want to Submit Details ?")) {
                            return true;
                        }
                        else {
                            return false;
                        }
                    }
                    if (document.getElementById('<%=btnSubmit.ClientID%>').value.trim() == "MODIFY") {
                        if (confirm("Do you really want to Modify Details ?")) {
                            return true;
                        }
                        else {
                            return false;
                        }
                    }
                }
            }
        </script>




    </div>
</asp:Content>

