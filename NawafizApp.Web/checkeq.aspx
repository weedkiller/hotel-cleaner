<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="checkeq.aspx.cs" Inherits="NawafizApp.Web.checkeq"  %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server" >
        <div aria-atomic="True" aria-busy="True" aria-checked="false" aria-live="assertive">
            <asp:GridView  ID="GridView1" runat="server" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" DataKeyNames="Id" DataSourceID="SqlDataSource1" Height="223px" Width="561px" BackColor="White" BorderColor="White" BorderStyle="Inset" CellPadding="0" ShowFooter="True">
                <Columns>
                    <asp:CommandField ShowEditButton="True" />
                    <asp:BoundField DataField="Id" HeaderText="Id" InsertVisible="False" ReadOnly="True" SortExpression="Id" />
                    <asp:BoundField DataField="Name" HeaderText="Name" SortExpression="Name" />
                    <asp:BoundField DataField="Description" HeaderText="Description" SortExpression="Description" />
                    <asp:BoundField DataField="Stauts" HeaderText="Stauts" SortExpression="Stauts" />
                    <asp:CheckBoxField DataField="needfix" HeaderText="needfix" SortExpression="needfix" />
                    <asp:CheckBoxField DataField="ishere" HeaderText="ishere" SortExpression="ishere" />
                    <asp:BoundField DataField="Room_Id" HeaderText="Room_Id" SortExpression="Room_Id" />
                </Columns>
                <EditRowStyle BorderColor="#CCCCCC" Font-Bold="True" Font-Names="Adobe Arabic" />
            </asp:GridView>
            <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConflictDetection="CompareAllValues" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" DeleteCommand="DELETE FROM [Equipments] WHERE [Id] = @original_Id AND (([Name] = @original_Name) OR ([Name] IS NULL AND @original_Name IS NULL)) AND (([Description] = @original_Description) OR ([Description] IS NULL AND @original_Description IS NULL)) AND (([Stauts] = @original_Stauts) OR ([Stauts] IS NULL AND @original_Stauts IS NULL)) AND [needfix] = @original_needfix AND [ishere] = @original_ishere AND (([Room_Id] = @original_Room_Id) OR ([Room_Id] IS NULL AND @original_Room_Id IS NULL))" InsertCommand="INSERT INTO [Equipments] ([Name], [Description], [Stauts], [needfix], [ishere], [Room_Id]) VALUES (@Name, @Description, @Stauts, @needfix, @ishere, @Room_Id)" OldValuesParameterFormatString="original_{0}" SelectCommand="SELECT * FROM [Equipments] WHERE ([Room_Id] = @Room_Id2)" UpdateCommand="UPDATE [Equipments] SET [Name] = @Name, [Description] = @Description, [Stauts] = @Stauts, [needfix] = @needfix, [ishere] = @ishere, [Room_Id] = @Room_Id WHERE [Id] = @original_Id AND (([Name] = @original_Name) OR ([Name] IS NULL AND @original_Name IS NULL)) AND (([Description] = @original_Description) OR ([Description] IS NULL AND @original_Description IS NULL)) AND (([Stauts] = @original_Stauts) OR ([Stauts] IS NULL AND @original_Stauts IS NULL)) AND [needfix] = @original_needfix AND [ishere] = @original_ishere AND (([Room_Id] = @original_Room_Id) OR ([Room_Id] IS NULL AND @original_Room_Id IS NULL))">
                <DeleteParameters>
                    <asp:Parameter Name="original_Id" Type="Int32" />
                    <asp:Parameter Name="original_Name" Type="String" />
                    <asp:Parameter Name="original_Description" Type="String" />
                    <asp:Parameter Name="original_Stauts" Type="String" />
                    <asp:Parameter Name="original_needfix" Type="Boolean" />
                    <asp:Parameter Name="original_ishere" Type="Boolean" />
                    <asp:Parameter Name="original_Room_Id" Type="Int32" />
                </DeleteParameters>
                <InsertParameters>
                    <asp:Parameter Name="Name" Type="String" />
                    <asp:Parameter Name="Description" Type="String" />
                    <asp:Parameter Name="Stauts" Type="String" />
                    <asp:Parameter Name="needfix" Type="Boolean" />
                    <asp:Parameter Name="ishere" Type="Boolean" />
                    <asp:Parameter Name="Room_Id" Type="Int32" />
                </InsertParameters>
                <SelectParameters>
                    <asp:RouteParameter Name="Room_Id2" RouteKey="id" Type="Int32" />
                </SelectParameters>
                <UpdateParameters>
                    <asp:Parameter Name="Name" Type="String" />
                    <asp:Parameter Name="Description" Type="String" />
                    <asp:Parameter Name="Stauts" Type="String" />
                    <asp:Parameter Name="needfix" Type="Boolean" />
                    <asp:Parameter Name="ishere" Type="Boolean" />
                    <asp:Parameter Name="Room_Id" Type="Int32" />
                    <asp:Parameter Name="original_Id" Type="Int32" />
                    <asp:Parameter Name="original_Name" Type="String" />
                    <asp:Parameter Name="original_Description" Type="String" />
                    <asp:Parameter Name="original_Stauts" Type="String" />
                    <asp:Parameter Name="original_needfix" Type="Boolean" />
                    <asp:Parameter Name="original_ishere" Type="Boolean" />
                    <asp:Parameter Name="original_Room_Id" Type="Int32" />
                </UpdateParameters>
            </asp:SqlDataSource>
        </div>
    </form>
</body>
</html>
