using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Constants
{
    public static class Messages
    {
        //Product
        public static string ProductAdded = "Product Added Successfully";
        public static string ProductNameInvalid = "Product Name Must Be A Minimum Of Two Characters";
        public static string MaintenanceTime = "Maintenance Time";
        public static string ProductsListed = "Products Listed Successfully";
        public static string ProductUpdated = "Product Updated Successfully";
        public static string UnitPriceInvalid = "Unit Price Invalid";
        public static string ProductCountOfCategoryError = "Bir Kategoride En Fazla 10 Ürün Olabilir";
        public static string ProductNameAlreadyExists = "Böyle Bir Ürün İsmi Zaten Var";
        internal static string CategoryLimitExceded="Kategori Limit'i Aşıldığı İçin Yeni Ürün Eklenemiyor";
    }
}
