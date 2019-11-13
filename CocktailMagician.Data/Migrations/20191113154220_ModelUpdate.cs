using Microsoft.EntityFrameworkCore.Migrations;

namespace CocktailMagician.Data.Migrations
{
    public partial class ModelUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CocktailIngredients_Ingredients_IngredientID",
                table: "CocktailIngredients");

            migrationBuilder.DropColumn(
                name: "IngredienetID",
                table: "CocktailIngredients");

            migrationBuilder.RenameColumn(
                name: "ID",
                table: "Cocktails",
                newName: "Id");

            migrationBuilder.AlterColumn<int>(
                name: "IngredientID",
                table: "CocktailIngredients",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_CocktailIngredients_Ingredients_IngredientID",
                table: "CocktailIngredients",
                column: "IngredientID",
                principalTable: "Ingredients",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CocktailIngredients_Ingredients_IngredientID",
                table: "CocktailIngredients");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Cocktails",
                newName: "ID");

            migrationBuilder.AlterColumn<int>(
                name: "IngredientID",
                table: "CocktailIngredients",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<int>(
                name: "IngredienetID",
                table: "CocktailIngredients",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_CocktailIngredients_Ingredients_IngredientID",
                table: "CocktailIngredients",
                column: "IngredientID",
                principalTable: "Ingredients",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
