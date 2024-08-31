<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="EmpPage.aspx.cs" Inherits="EmployeeCrudOperations.EmpPage" %>
<%--<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container">
        <div class="row">
            <div class = "col-md-6">
                <asp:GridView ID = "GridViewEmployees" runat = "server" AutoGenerateColumns =" false" CssClass="table table-striped table-bordered table-hover">
                    <Columns>
                        <asp:BoundField DataField = "Id" HeaderText = "ID" />
                        <asp:BoundField DataField = "EmpName" HeaderText = "EmployeeName" />
                        <asp:BoundField DataField = "Position" HeaderText = "Position" />
                        <asp:BoundField DataField = "Salary" HeaderText = "Salary" />
                        <asp:TemplateField HeaderText="Image">
                        <ItemTemplate>
                            <asp:Image ID="imgEmployee" runat="server" Width="50px" Height="50px" ImageUrl='<%# Eval("ImageUrl") %>' />
                        </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>

            <div class = "col-md-6">
                EmployeeId: <asp:TextBox ID = "txtId" runat = "server"  CssClass="form-control"></asp:TextBox> <br />
                EmployeeName: <asp:TextBox ID = "txtEmpName" runat = "server" CssClass="form-control"></asp:TextBox> <br />
                Position: <asp:TextBox ID = "txtPosition" runat = "server" CssClass="form-control"></asp:TextBox> <br />
                Salary: <asp:TextBox ID = "txtSalary" runat = "server" CssClass="form-control"></asp:TextBox> <br />
                
                <!-- Image Upload and Display -->
    <asp:FileUpload ID="FileUploadImage" runat="server" CssClass="form-control" /><br />
    <asp:Image ID="ImageEmployee" runat="server" CssClass="img-thumbnail" Width="100px" Height="100px" /><br />
                
                <asp:Button ID ="btnAdd" runat ="server" Text ="Add Employee" OnClick="btnAdd_Click" cssclass="btn btn-primary" />
                <asp:Button ID ="btnUpdate" runat="server" Text ="Update Employee" OnClick="btnUpdate_Click" cssclass="btn btn-primary" />
                <asp:Button ID ="btnDelete" runat ="server" Text ="Delete Employee" OnClick="btnDelete_Click" cssclass="btn btn-primary" />
                <asp:FileUpload ID="fileUpload" runat="server" CssClass="form-control" />
                <asp:Button ID="btnUpload" runat="server" Text="Upload" OnClick="btnUpload_Click" CssClass="btn btn-primary" />

            </div>

        </div>
    </div>--%>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container">
        <div class="row">
            <div class="col-md-6">
                <asp:GridView ID="GridViewEmployees" runat="server" AutoGenerateColumns="false" CssClass="table table-striped table-bordered table-hover">
                    <Columns>
                        <asp:BoundField DataField="Id" HeaderText="ID" />
                        <asp:BoundField DataField="EmpName" HeaderText="EmployeeName" />
                        <asp:BoundField DataField="Position" HeaderText="Position" />
                        <asp:BoundField DataField="Salary" HeaderText="Salary" />
                        <asp:TemplateField HeaderText="Employee Image">
                            <ItemTemplate>
                                <asp:Image ID="imgEmployee" runat="server" Width="50px" Height="50px" ImageUrl='<%# Eval("ImageUrl") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>

            <div class="col-md-6">
                EmployeeId: <asp:TextBox ID="txtId" runat="server" CssClass="form-control"></asp:TextBox> <br />
                EmployeeName: <asp:TextBox ID="txtEmpName" runat="server" CssClass="form-control"></asp:TextBox> <br />
                Position: <asp:TextBox ID="txtPosition" runat="server" CssClass="form-control"></asp:TextBox> <br />
                Salary: <asp:TextBox ID="txtSalary" runat="server" CssClass="form-control"></asp:TextBox> <br />
                <asp:FileUpload ID="FileUploadImage" runat="server" CssClass="form-control" /><br />
                <asp:Button ID="btnAdd" runat="server" Text="Add Employee" OnClick="btnAdd_Click" CssClass="btn btn-primary" />
                <asp:Button ID="btnUpdate" runat="server" Text="Update Employee" OnClick="btnUpdate_Click" CssClass="btn btn-primary" />
                <asp:Button ID="btnDelete" runat="server" Text="Delete Employee" OnClick="btnDelete_Click" CssClass="btn btn-primary" />
                <asp:Label ID="lblMessage" runat="server" CssClass="text-danger"></asp:Label>
            </div>
        </div>
    </div>
</asp:Content>


<%--</asp:Content>--%>
