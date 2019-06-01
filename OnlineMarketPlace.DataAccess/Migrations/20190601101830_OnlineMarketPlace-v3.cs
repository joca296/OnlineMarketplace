using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace OnlineMarketPlace.DataAccess.Migrations
{
    public partial class OnlineMarketPlacev3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Images_Products_Productsid",
                table: "Images");

            migrationBuilder.DropIndex(
                name: "IX_Images_Productsid",
                table: "Images");

            migrationBuilder.DropColumn(
                name: "Productsid",
                table: "Images");

            migrationBuilder.AddColumn<double>(
                name: "unitWeight",
                table: "Products",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.CreateTable(
                name: "Coupons",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    dateCreated = table.Column<DateTime>(nullable: false),
                    dateUpdated = table.Column<DateTime>(nullable: true),
                    active = table.Column<bool>(nullable: false),
                    key = table.Column<string>(nullable: true),
                    name = table.Column<string>(nullable: true),
                    code = table.Column<string>(nullable: true),
                    discount = table.Column<double>(nullable: false),
                    freeShipping = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Coupons", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "ProductImages",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    dateCreated = table.Column<DateTime>(nullable: false),
                    dateUpdated = table.Column<DateTime>(nullable: true),
                    active = table.Column<bool>(nullable: false),
                    key = table.Column<string>(nullable: true),
                    productid = table.Column<int>(nullable: true),
                    imageid = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductImages", x => x.id);
                    table.ForeignKey(
                        name: "FK_ProductImages_Images_imageid",
                        column: x => x.imageid,
                        principalTable: "Images",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProductImages_Products_productid",
                        column: x => x.productid,
                        principalTable: "Products",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Shippers",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    dateCreated = table.Column<DateTime>(nullable: false),
                    dateUpdated = table.Column<DateTime>(nullable: true),
                    active = table.Column<bool>(nullable: false),
                    key = table.Column<string>(nullable: true),
                    name = table.Column<string>(nullable: true),
                    freightPerKilo = table.Column<double>(nullable: false),
                    freightBase = table.Column<double>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Shippers", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    dateCreated = table.Column<DateTime>(nullable: false),
                    dateUpdated = table.Column<DateTime>(nullable: true),
                    active = table.Column<bool>(nullable: false),
                    key = table.Column<string>(nullable: true),
                    totalPrice = table.Column<double>(nullable: false),
                    totalFreight = table.Column<double>(nullable: false),
                    userid = table.Column<int>(nullable: true),
                    shipperid = table.Column<int>(nullable: true),
                    shippingAddressid = table.Column<int>(nullable: true),
                    dateShipped = table.Column<DateTime>(nullable: true),
                    dateDelivered = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.id);
                    table.ForeignKey(
                        name: "FK_Orders_Shippers_shipperid",
                        column: x => x.shipperid,
                        principalTable: "Shippers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Orders_ShippingAddresses_shippingAddressid",
                        column: x => x.shippingAddressid,
                        principalTable: "ShippingAddresses",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Orders_Users_userid",
                        column: x => x.userid,
                        principalTable: "Users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "OrderCoupons",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    dateCreated = table.Column<DateTime>(nullable: false),
                    dateUpdated = table.Column<DateTime>(nullable: true),
                    active = table.Column<bool>(nullable: false),
                    key = table.Column<string>(nullable: true),
                    orderid = table.Column<int>(nullable: true),
                    couponid = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderCoupons", x => x.id);
                    table.ForeignKey(
                        name: "FK_OrderCoupons_Coupons_couponid",
                        column: x => x.couponid,
                        principalTable: "Coupons",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OrderCoupons_Orders_orderid",
                        column: x => x.orderid,
                        principalTable: "Orders",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "OrderProducts",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    dateCreated = table.Column<DateTime>(nullable: false),
                    dateUpdated = table.Column<DateTime>(nullable: true),
                    active = table.Column<bool>(nullable: false),
                    key = table.Column<string>(nullable: true),
                    orderid = table.Column<int>(nullable: true),
                    productid = table.Column<int>(nullable: true),
                    quantity = table.Column<int>(nullable: false),
                    totalWeight = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderProducts", x => x.id);
                    table.ForeignKey(
                        name: "FK_OrderProducts_Orders_orderid",
                        column: x => x.orderid,
                        principalTable: "Orders",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OrderProducts_Products_productid",
                        column: x => x.productid,
                        principalTable: "Products",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrderCoupons_couponid",
                table: "OrderCoupons",
                column: "couponid");

            migrationBuilder.CreateIndex(
                name: "IX_OrderCoupons_orderid",
                table: "OrderCoupons",
                column: "orderid");

            migrationBuilder.CreateIndex(
                name: "IX_OrderProducts_orderid",
                table: "OrderProducts",
                column: "orderid");

            migrationBuilder.CreateIndex(
                name: "IX_OrderProducts_productid",
                table: "OrderProducts",
                column: "productid");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_shipperid",
                table: "Orders",
                column: "shipperid");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_shippingAddressid",
                table: "Orders",
                column: "shippingAddressid");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_userid",
                table: "Orders",
                column: "userid");

            migrationBuilder.CreateIndex(
                name: "IX_ProductImages_imageid",
                table: "ProductImages",
                column: "imageid");

            migrationBuilder.CreateIndex(
                name: "IX_ProductImages_productid",
                table: "ProductImages",
                column: "productid");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderCoupons");

            migrationBuilder.DropTable(
                name: "OrderProducts");

            migrationBuilder.DropTable(
                name: "ProductImages");

            migrationBuilder.DropTable(
                name: "Coupons");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Shippers");

            migrationBuilder.DropColumn(
                name: "unitWeight",
                table: "Products");

            migrationBuilder.AddColumn<int>(
                name: "Productsid",
                table: "Images",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Images_Productsid",
                table: "Images",
                column: "Productsid");

            migrationBuilder.AddForeignKey(
                name: "FK_Images_Products_Productsid",
                table: "Images",
                column: "Productsid",
                principalTable: "Products",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
