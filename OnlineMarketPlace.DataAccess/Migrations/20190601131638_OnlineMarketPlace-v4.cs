using Microsoft.EntityFrameworkCore.Migrations;

namespace OnlineMarketPlace.DataAccess.Migrations
{
    public partial class OnlineMarketPlacev4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderCoupons_Coupons_couponid",
                table: "OrderCoupons");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderCoupons_Orders_orderid",
                table: "OrderCoupons");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderProducts_Orders_orderid",
                table: "OrderProducts");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderProducts_Products_productid",
                table: "OrderProducts");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Shippers_shipperid",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_ShippingAddresses_shippingAddressid",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Users_userid",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductImages_Images_imageid",
                table: "ProductImages");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductImages_Products_productid",
                table: "ProductImages");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_Categories_categoryid",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_ShippingAddresses_Users_userid",
                table: "ShippingAddresses");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Roles_roleid",
                table: "Users");

            migrationBuilder.RenameColumn(
                name: "roleid",
                table: "Users",
                newName: "RoleId");

            migrationBuilder.RenameColumn(
                name: "password",
                table: "Users",
                newName: "Password");

            migrationBuilder.RenameColumn(
                name: "lastName",
                table: "Users",
                newName: "LastName");

            migrationBuilder.RenameColumn(
                name: "key",
                table: "Users",
                newName: "Key");

            migrationBuilder.RenameColumn(
                name: "firstName",
                table: "Users",
                newName: "FirstName");

            migrationBuilder.RenameColumn(
                name: "eMail",
                table: "Users",
                newName: "Email");

            migrationBuilder.RenameColumn(
                name: "dateUpdated",
                table: "Users",
                newName: "DateUpdated");

            migrationBuilder.RenameColumn(
                name: "dateCreated",
                table: "Users",
                newName: "DateCreated");

            migrationBuilder.RenameColumn(
                name: "active",
                table: "Users",
                newName: "Active");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Users",
                newName: "Id");

            migrationBuilder.RenameIndex(
                name: "IX_Users_roleid",
                table: "Users",
                newName: "IX_Users_RoleId");

            migrationBuilder.RenameColumn(
                name: "userid",
                table: "ShippingAddresses",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "key",
                table: "ShippingAddresses",
                newName: "Key");

            migrationBuilder.RenameColumn(
                name: "dateUpdated",
                table: "ShippingAddresses",
                newName: "DateUpdated");

            migrationBuilder.RenameColumn(
                name: "dateCreated",
                table: "ShippingAddresses",
                newName: "DateCreated");

            migrationBuilder.RenameColumn(
                name: "active",
                table: "ShippingAddresses",
                newName: "Active");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "ShippingAddresses",
                newName: "Id");

            migrationBuilder.RenameIndex(
                name: "IX_ShippingAddresses_userid",
                table: "ShippingAddresses",
                newName: "IX_ShippingAddresses_UserId");

            migrationBuilder.RenameColumn(
                name: "name",
                table: "Shippers",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "key",
                table: "Shippers",
                newName: "Key");

            migrationBuilder.RenameColumn(
                name: "freightPerKilo",
                table: "Shippers",
                newName: "FreightPerKilo");

            migrationBuilder.RenameColumn(
                name: "freightBase",
                table: "Shippers",
                newName: "FreightBase");

            migrationBuilder.RenameColumn(
                name: "dateUpdated",
                table: "Shippers",
                newName: "DateUpdated");

            migrationBuilder.RenameColumn(
                name: "dateCreated",
                table: "Shippers",
                newName: "DateCreated");

            migrationBuilder.RenameColumn(
                name: "active",
                table: "Shippers",
                newName: "Active");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Shippers",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "name",
                table: "Roles",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "key",
                table: "Roles",
                newName: "Key");

            migrationBuilder.RenameColumn(
                name: "dateUpdated",
                table: "Roles",
                newName: "DateUpdated");

            migrationBuilder.RenameColumn(
                name: "dateCreated",
                table: "Roles",
                newName: "DateCreated");

            migrationBuilder.RenameColumn(
                name: "active",
                table: "Roles",
                newName: "Active");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Roles",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "unitWeight",
                table: "Products",
                newName: "UnitWeight");

            migrationBuilder.RenameColumn(
                name: "unitPrice",
                table: "Products",
                newName: "UnitPrice");

            migrationBuilder.RenameColumn(
                name: "quantityAvailable",
                table: "Products",
                newName: "QuantityAvailable");

            migrationBuilder.RenameColumn(
                name: "name",
                table: "Products",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "key",
                table: "Products",
                newName: "Key");

            migrationBuilder.RenameColumn(
                name: "description",
                table: "Products",
                newName: "Description");

            migrationBuilder.RenameColumn(
                name: "dateUpdated",
                table: "Products",
                newName: "DateUpdated");

            migrationBuilder.RenameColumn(
                name: "dateCreated",
                table: "Products",
                newName: "DateCreated");

            migrationBuilder.RenameColumn(
                name: "categoryid",
                table: "Products",
                newName: "CategoryId");

            migrationBuilder.RenameColumn(
                name: "active",
                table: "Products",
                newName: "Active");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Products",
                newName: "Id");

            migrationBuilder.RenameIndex(
                name: "IX_Products_categoryid",
                table: "Products",
                newName: "IX_Products_CategoryId");

            migrationBuilder.RenameColumn(
                name: "productid",
                table: "ProductImages",
                newName: "ProductId");

            migrationBuilder.RenameColumn(
                name: "key",
                table: "ProductImages",
                newName: "Key");

            migrationBuilder.RenameColumn(
                name: "imageid",
                table: "ProductImages",
                newName: "ImageId");

            migrationBuilder.RenameColumn(
                name: "dateUpdated",
                table: "ProductImages",
                newName: "DateUpdated");

            migrationBuilder.RenameColumn(
                name: "dateCreated",
                table: "ProductImages",
                newName: "DateCreated");

            migrationBuilder.RenameColumn(
                name: "active",
                table: "ProductImages",
                newName: "Active");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "ProductImages",
                newName: "Id");

            migrationBuilder.RenameIndex(
                name: "IX_ProductImages_productid",
                table: "ProductImages",
                newName: "IX_ProductImages_ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_ProductImages_imageid",
                table: "ProductImages",
                newName: "IX_ProductImages_ImageId");

            migrationBuilder.RenameColumn(
                name: "userid",
                table: "Orders",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "totalPrice",
                table: "Orders",
                newName: "TotalPrice");

            migrationBuilder.RenameColumn(
                name: "totalFreight",
                table: "Orders",
                newName: "TotalFreight");

            migrationBuilder.RenameColumn(
                name: "shippingAddressid",
                table: "Orders",
                newName: "ShippingAddressId");

            migrationBuilder.RenameColumn(
                name: "shipperid",
                table: "Orders",
                newName: "ShipperId");

            migrationBuilder.RenameColumn(
                name: "key",
                table: "Orders",
                newName: "Key");

            migrationBuilder.RenameColumn(
                name: "dateUpdated",
                table: "Orders",
                newName: "DateUpdated");

            migrationBuilder.RenameColumn(
                name: "dateShipped",
                table: "Orders",
                newName: "DateShipped");

            migrationBuilder.RenameColumn(
                name: "dateDelivered",
                table: "Orders",
                newName: "DateDelivered");

            migrationBuilder.RenameColumn(
                name: "dateCreated",
                table: "Orders",
                newName: "DateCreated");

            migrationBuilder.RenameColumn(
                name: "active",
                table: "Orders",
                newName: "Active");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Orders",
                newName: "Id");

            migrationBuilder.RenameIndex(
                name: "IX_Orders_userid",
                table: "Orders",
                newName: "IX_Orders_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Orders_shippingAddressid",
                table: "Orders",
                newName: "IX_Orders_ShippingAddressId");

            migrationBuilder.RenameIndex(
                name: "IX_Orders_shipperid",
                table: "Orders",
                newName: "IX_Orders_ShipperId");

            migrationBuilder.RenameColumn(
                name: "totalWeight",
                table: "OrderProducts",
                newName: "TotalWeight");

            migrationBuilder.RenameColumn(
                name: "quantity",
                table: "OrderProducts",
                newName: "Quantity");

            migrationBuilder.RenameColumn(
                name: "productid",
                table: "OrderProducts",
                newName: "ProductId");

            migrationBuilder.RenameColumn(
                name: "orderid",
                table: "OrderProducts",
                newName: "OrderId");

            migrationBuilder.RenameColumn(
                name: "key",
                table: "OrderProducts",
                newName: "Key");

            migrationBuilder.RenameColumn(
                name: "dateUpdated",
                table: "OrderProducts",
                newName: "DateUpdated");

            migrationBuilder.RenameColumn(
                name: "dateCreated",
                table: "OrderProducts",
                newName: "DateCreated");

            migrationBuilder.RenameColumn(
                name: "active",
                table: "OrderProducts",
                newName: "Active");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "OrderProducts",
                newName: "Id");

            migrationBuilder.RenameIndex(
                name: "IX_OrderProducts_productid",
                table: "OrderProducts",
                newName: "IX_OrderProducts_ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_OrderProducts_orderid",
                table: "OrderProducts",
                newName: "IX_OrderProducts_OrderId");

            migrationBuilder.RenameColumn(
                name: "orderid",
                table: "OrderCoupons",
                newName: "OrderId");

            migrationBuilder.RenameColumn(
                name: "key",
                table: "OrderCoupons",
                newName: "Key");

            migrationBuilder.RenameColumn(
                name: "dateUpdated",
                table: "OrderCoupons",
                newName: "DateUpdated");

            migrationBuilder.RenameColumn(
                name: "dateCreated",
                table: "OrderCoupons",
                newName: "DateCreated");

            migrationBuilder.RenameColumn(
                name: "couponid",
                table: "OrderCoupons",
                newName: "CouponId");

            migrationBuilder.RenameColumn(
                name: "active",
                table: "OrderCoupons",
                newName: "Active");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "OrderCoupons",
                newName: "Id");

            migrationBuilder.RenameIndex(
                name: "IX_OrderCoupons_orderid",
                table: "OrderCoupons",
                newName: "IX_OrderCoupons_OrderId");

            migrationBuilder.RenameIndex(
                name: "IX_OrderCoupons_couponid",
                table: "OrderCoupons",
                newName: "IX_OrderCoupons_CouponId");

            migrationBuilder.RenameColumn(
                name: "path",
                table: "Images",
                newName: "Path");

            migrationBuilder.RenameColumn(
                name: "key",
                table: "Images",
                newName: "Key");

            migrationBuilder.RenameColumn(
                name: "dateUpdated",
                table: "Images",
                newName: "DateUpdated");

            migrationBuilder.RenameColumn(
                name: "dateCreated",
                table: "Images",
                newName: "DateCreated");

            migrationBuilder.RenameColumn(
                name: "alt",
                table: "Images",
                newName: "Alt");

            migrationBuilder.RenameColumn(
                name: "active",
                table: "Images",
                newName: "Active");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Images",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "name",
                table: "Coupons",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "key",
                table: "Coupons",
                newName: "Key");

            migrationBuilder.RenameColumn(
                name: "freeShipping",
                table: "Coupons",
                newName: "FreeShipping");

            migrationBuilder.RenameColumn(
                name: "discount",
                table: "Coupons",
                newName: "Discount");

            migrationBuilder.RenameColumn(
                name: "dateUpdated",
                table: "Coupons",
                newName: "DateUpdated");

            migrationBuilder.RenameColumn(
                name: "dateCreated",
                table: "Coupons",
                newName: "DateCreated");

            migrationBuilder.RenameColumn(
                name: "code",
                table: "Coupons",
                newName: "Code");

            migrationBuilder.RenameColumn(
                name: "active",
                table: "Coupons",
                newName: "Active");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Coupons",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "name",
                table: "Categories",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "key",
                table: "Categories",
                newName: "Key");

            migrationBuilder.RenameColumn(
                name: "dateUpdated",
                table: "Categories",
                newName: "DateUpdated");

            migrationBuilder.RenameColumn(
                name: "dateCreated",
                table: "Categories",
                newName: "DateCreated");

            migrationBuilder.RenameColumn(
                name: "active",
                table: "Categories",
                newName: "Active");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Categories",
                newName: "Id");

            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "ShippingAddresses",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "City",
                table: "ShippingAddresses",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Country",
                table: "ShippingAddresses",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PostalCode",
                table: "ShippingAddresses",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "State",
                table: "ShippingAddresses",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "TotalPrice",
                table: "OrderProducts",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderCoupons_Coupons_CouponId",
                table: "OrderCoupons",
                column: "CouponId",
                principalTable: "Coupons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderCoupons_Orders_OrderId",
                table: "OrderCoupons",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderProducts_Orders_OrderId",
                table: "OrderProducts",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderProducts_Products_ProductId",
                table: "OrderProducts",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Shippers_ShipperId",
                table: "Orders",
                column: "ShipperId",
                principalTable: "Shippers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_ShippingAddresses_ShippingAddressId",
                table: "Orders",
                column: "ShippingAddressId",
                principalTable: "ShippingAddresses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Users_UserId",
                table: "Orders",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductImages_Images_ImageId",
                table: "ProductImages",
                column: "ImageId",
                principalTable: "Images",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductImages_Products_ProductId",
                table: "ProductImages",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Categories_CategoryId",
                table: "Products",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ShippingAddresses_Users_UserId",
                table: "ShippingAddresses",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Roles_RoleId",
                table: "Users",
                column: "RoleId",
                principalTable: "Roles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderCoupons_Coupons_CouponId",
                table: "OrderCoupons");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderCoupons_Orders_OrderId",
                table: "OrderCoupons");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderProducts_Orders_OrderId",
                table: "OrderProducts");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderProducts_Products_ProductId",
                table: "OrderProducts");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Shippers_ShipperId",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_ShippingAddresses_ShippingAddressId",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Users_UserId",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductImages_Images_ImageId",
                table: "ProductImages");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductImages_Products_ProductId",
                table: "ProductImages");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_Categories_CategoryId",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_ShippingAddresses_Users_UserId",
                table: "ShippingAddresses");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Roles_RoleId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Address",
                table: "ShippingAddresses");

            migrationBuilder.DropColumn(
                name: "City",
                table: "ShippingAddresses");

            migrationBuilder.DropColumn(
                name: "Country",
                table: "ShippingAddresses");

            migrationBuilder.DropColumn(
                name: "PostalCode",
                table: "ShippingAddresses");

            migrationBuilder.DropColumn(
                name: "State",
                table: "ShippingAddresses");

            migrationBuilder.DropColumn(
                name: "TotalPrice",
                table: "OrderProducts");

            migrationBuilder.RenameColumn(
                name: "RoleId",
                table: "Users",
                newName: "roleid");

            migrationBuilder.RenameColumn(
                name: "Password",
                table: "Users",
                newName: "password");

            migrationBuilder.RenameColumn(
                name: "LastName",
                table: "Users",
                newName: "lastName");

            migrationBuilder.RenameColumn(
                name: "Key",
                table: "Users",
                newName: "key");

            migrationBuilder.RenameColumn(
                name: "FirstName",
                table: "Users",
                newName: "firstName");

            migrationBuilder.RenameColumn(
                name: "Email",
                table: "Users",
                newName: "eMail");

            migrationBuilder.RenameColumn(
                name: "DateUpdated",
                table: "Users",
                newName: "dateUpdated");

            migrationBuilder.RenameColumn(
                name: "DateCreated",
                table: "Users",
                newName: "dateCreated");

            migrationBuilder.RenameColumn(
                name: "Active",
                table: "Users",
                newName: "active");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Users",
                newName: "id");

            migrationBuilder.RenameIndex(
                name: "IX_Users_RoleId",
                table: "Users",
                newName: "IX_Users_roleid");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "ShippingAddresses",
                newName: "userid");

            migrationBuilder.RenameColumn(
                name: "Key",
                table: "ShippingAddresses",
                newName: "key");

            migrationBuilder.RenameColumn(
                name: "DateUpdated",
                table: "ShippingAddresses",
                newName: "dateUpdated");

            migrationBuilder.RenameColumn(
                name: "DateCreated",
                table: "ShippingAddresses",
                newName: "dateCreated");

            migrationBuilder.RenameColumn(
                name: "Active",
                table: "ShippingAddresses",
                newName: "active");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "ShippingAddresses",
                newName: "id");

            migrationBuilder.RenameIndex(
                name: "IX_ShippingAddresses_UserId",
                table: "ShippingAddresses",
                newName: "IX_ShippingAddresses_userid");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Shippers",
                newName: "name");

            migrationBuilder.RenameColumn(
                name: "Key",
                table: "Shippers",
                newName: "key");

            migrationBuilder.RenameColumn(
                name: "FreightPerKilo",
                table: "Shippers",
                newName: "freightPerKilo");

            migrationBuilder.RenameColumn(
                name: "FreightBase",
                table: "Shippers",
                newName: "freightBase");

            migrationBuilder.RenameColumn(
                name: "DateUpdated",
                table: "Shippers",
                newName: "dateUpdated");

            migrationBuilder.RenameColumn(
                name: "DateCreated",
                table: "Shippers",
                newName: "dateCreated");

            migrationBuilder.RenameColumn(
                name: "Active",
                table: "Shippers",
                newName: "active");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Shippers",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Roles",
                newName: "name");

            migrationBuilder.RenameColumn(
                name: "Key",
                table: "Roles",
                newName: "key");

            migrationBuilder.RenameColumn(
                name: "DateUpdated",
                table: "Roles",
                newName: "dateUpdated");

            migrationBuilder.RenameColumn(
                name: "DateCreated",
                table: "Roles",
                newName: "dateCreated");

            migrationBuilder.RenameColumn(
                name: "Active",
                table: "Roles",
                newName: "active");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Roles",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "UnitWeight",
                table: "Products",
                newName: "unitWeight");

            migrationBuilder.RenameColumn(
                name: "UnitPrice",
                table: "Products",
                newName: "unitPrice");

            migrationBuilder.RenameColumn(
                name: "QuantityAvailable",
                table: "Products",
                newName: "quantityAvailable");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Products",
                newName: "name");

            migrationBuilder.RenameColumn(
                name: "Key",
                table: "Products",
                newName: "key");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "Products",
                newName: "description");

            migrationBuilder.RenameColumn(
                name: "DateUpdated",
                table: "Products",
                newName: "dateUpdated");

            migrationBuilder.RenameColumn(
                name: "DateCreated",
                table: "Products",
                newName: "dateCreated");

            migrationBuilder.RenameColumn(
                name: "CategoryId",
                table: "Products",
                newName: "categoryid");

            migrationBuilder.RenameColumn(
                name: "Active",
                table: "Products",
                newName: "active");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Products",
                newName: "id");

            migrationBuilder.RenameIndex(
                name: "IX_Products_CategoryId",
                table: "Products",
                newName: "IX_Products_categoryid");

            migrationBuilder.RenameColumn(
                name: "ProductId",
                table: "ProductImages",
                newName: "productid");

            migrationBuilder.RenameColumn(
                name: "Key",
                table: "ProductImages",
                newName: "key");

            migrationBuilder.RenameColumn(
                name: "ImageId",
                table: "ProductImages",
                newName: "imageid");

            migrationBuilder.RenameColumn(
                name: "DateUpdated",
                table: "ProductImages",
                newName: "dateUpdated");

            migrationBuilder.RenameColumn(
                name: "DateCreated",
                table: "ProductImages",
                newName: "dateCreated");

            migrationBuilder.RenameColumn(
                name: "Active",
                table: "ProductImages",
                newName: "active");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "ProductImages",
                newName: "id");

            migrationBuilder.RenameIndex(
                name: "IX_ProductImages_ProductId",
                table: "ProductImages",
                newName: "IX_ProductImages_productid");

            migrationBuilder.RenameIndex(
                name: "IX_ProductImages_ImageId",
                table: "ProductImages",
                newName: "IX_ProductImages_imageid");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Orders",
                newName: "userid");

            migrationBuilder.RenameColumn(
                name: "TotalPrice",
                table: "Orders",
                newName: "totalPrice");

            migrationBuilder.RenameColumn(
                name: "TotalFreight",
                table: "Orders",
                newName: "totalFreight");

            migrationBuilder.RenameColumn(
                name: "ShippingAddressId",
                table: "Orders",
                newName: "shippingAddressid");

            migrationBuilder.RenameColumn(
                name: "ShipperId",
                table: "Orders",
                newName: "shipperid");

            migrationBuilder.RenameColumn(
                name: "Key",
                table: "Orders",
                newName: "key");

            migrationBuilder.RenameColumn(
                name: "DateUpdated",
                table: "Orders",
                newName: "dateUpdated");

            migrationBuilder.RenameColumn(
                name: "DateShipped",
                table: "Orders",
                newName: "dateShipped");

            migrationBuilder.RenameColumn(
                name: "DateDelivered",
                table: "Orders",
                newName: "dateDelivered");

            migrationBuilder.RenameColumn(
                name: "DateCreated",
                table: "Orders",
                newName: "dateCreated");

            migrationBuilder.RenameColumn(
                name: "Active",
                table: "Orders",
                newName: "active");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Orders",
                newName: "id");

            migrationBuilder.RenameIndex(
                name: "IX_Orders_UserId",
                table: "Orders",
                newName: "IX_Orders_userid");

            migrationBuilder.RenameIndex(
                name: "IX_Orders_ShippingAddressId",
                table: "Orders",
                newName: "IX_Orders_shippingAddressid");

            migrationBuilder.RenameIndex(
                name: "IX_Orders_ShipperId",
                table: "Orders",
                newName: "IX_Orders_shipperid");

            migrationBuilder.RenameColumn(
                name: "TotalWeight",
                table: "OrderProducts",
                newName: "totalWeight");

            migrationBuilder.RenameColumn(
                name: "Quantity",
                table: "OrderProducts",
                newName: "quantity");

            migrationBuilder.RenameColumn(
                name: "ProductId",
                table: "OrderProducts",
                newName: "productid");

            migrationBuilder.RenameColumn(
                name: "OrderId",
                table: "OrderProducts",
                newName: "orderid");

            migrationBuilder.RenameColumn(
                name: "Key",
                table: "OrderProducts",
                newName: "key");

            migrationBuilder.RenameColumn(
                name: "DateUpdated",
                table: "OrderProducts",
                newName: "dateUpdated");

            migrationBuilder.RenameColumn(
                name: "DateCreated",
                table: "OrderProducts",
                newName: "dateCreated");

            migrationBuilder.RenameColumn(
                name: "Active",
                table: "OrderProducts",
                newName: "active");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "OrderProducts",
                newName: "id");

            migrationBuilder.RenameIndex(
                name: "IX_OrderProducts_ProductId",
                table: "OrderProducts",
                newName: "IX_OrderProducts_productid");

            migrationBuilder.RenameIndex(
                name: "IX_OrderProducts_OrderId",
                table: "OrderProducts",
                newName: "IX_OrderProducts_orderid");

            migrationBuilder.RenameColumn(
                name: "OrderId",
                table: "OrderCoupons",
                newName: "orderid");

            migrationBuilder.RenameColumn(
                name: "Key",
                table: "OrderCoupons",
                newName: "key");

            migrationBuilder.RenameColumn(
                name: "DateUpdated",
                table: "OrderCoupons",
                newName: "dateUpdated");

            migrationBuilder.RenameColumn(
                name: "DateCreated",
                table: "OrderCoupons",
                newName: "dateCreated");

            migrationBuilder.RenameColumn(
                name: "CouponId",
                table: "OrderCoupons",
                newName: "couponid");

            migrationBuilder.RenameColumn(
                name: "Active",
                table: "OrderCoupons",
                newName: "active");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "OrderCoupons",
                newName: "id");

            migrationBuilder.RenameIndex(
                name: "IX_OrderCoupons_OrderId",
                table: "OrderCoupons",
                newName: "IX_OrderCoupons_orderid");

            migrationBuilder.RenameIndex(
                name: "IX_OrderCoupons_CouponId",
                table: "OrderCoupons",
                newName: "IX_OrderCoupons_couponid");

            migrationBuilder.RenameColumn(
                name: "Path",
                table: "Images",
                newName: "path");

            migrationBuilder.RenameColumn(
                name: "Key",
                table: "Images",
                newName: "key");

            migrationBuilder.RenameColumn(
                name: "DateUpdated",
                table: "Images",
                newName: "dateUpdated");

            migrationBuilder.RenameColumn(
                name: "DateCreated",
                table: "Images",
                newName: "dateCreated");

            migrationBuilder.RenameColumn(
                name: "Alt",
                table: "Images",
                newName: "alt");

            migrationBuilder.RenameColumn(
                name: "Active",
                table: "Images",
                newName: "active");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Images",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Coupons",
                newName: "name");

            migrationBuilder.RenameColumn(
                name: "Key",
                table: "Coupons",
                newName: "key");

            migrationBuilder.RenameColumn(
                name: "FreeShipping",
                table: "Coupons",
                newName: "freeShipping");

            migrationBuilder.RenameColumn(
                name: "Discount",
                table: "Coupons",
                newName: "discount");

            migrationBuilder.RenameColumn(
                name: "DateUpdated",
                table: "Coupons",
                newName: "dateUpdated");

            migrationBuilder.RenameColumn(
                name: "DateCreated",
                table: "Coupons",
                newName: "dateCreated");

            migrationBuilder.RenameColumn(
                name: "Code",
                table: "Coupons",
                newName: "code");

            migrationBuilder.RenameColumn(
                name: "Active",
                table: "Coupons",
                newName: "active");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Coupons",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Categories",
                newName: "name");

            migrationBuilder.RenameColumn(
                name: "Key",
                table: "Categories",
                newName: "key");

            migrationBuilder.RenameColumn(
                name: "DateUpdated",
                table: "Categories",
                newName: "dateUpdated");

            migrationBuilder.RenameColumn(
                name: "DateCreated",
                table: "Categories",
                newName: "dateCreated");

            migrationBuilder.RenameColumn(
                name: "Active",
                table: "Categories",
                newName: "active");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Categories",
                newName: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderCoupons_Coupons_couponid",
                table: "OrderCoupons",
                column: "couponid",
                principalTable: "Coupons",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderCoupons_Orders_orderid",
                table: "OrderCoupons",
                column: "orderid",
                principalTable: "Orders",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderProducts_Orders_orderid",
                table: "OrderProducts",
                column: "orderid",
                principalTable: "Orders",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderProducts_Products_productid",
                table: "OrderProducts",
                column: "productid",
                principalTable: "Products",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Shippers_shipperid",
                table: "Orders",
                column: "shipperid",
                principalTable: "Shippers",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_ShippingAddresses_shippingAddressid",
                table: "Orders",
                column: "shippingAddressid",
                principalTable: "ShippingAddresses",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Users_userid",
                table: "Orders",
                column: "userid",
                principalTable: "Users",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductImages_Images_imageid",
                table: "ProductImages",
                column: "imageid",
                principalTable: "Images",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductImages_Products_productid",
                table: "ProductImages",
                column: "productid",
                principalTable: "Products",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Categories_categoryid",
                table: "Products",
                column: "categoryid",
                principalTable: "Categories",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ShippingAddresses_Users_userid",
                table: "ShippingAddresses",
                column: "userid",
                principalTable: "Users",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Roles_roleid",
                table: "Users",
                column: "roleid",
                principalTable: "Roles",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
