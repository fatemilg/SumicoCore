using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SCMCore.Classes
{
    public class EventName
    {
        public enum ListofEvents
        {
//menu = Product.aspx
            //گروه محصولات
            AddProductCategory,                           // ثبت دسته بندی محصول
            EditProductCategory,                          //ویرایش دسته بندی محصول
            DeleteProductCategory,                        //حذف دسته بندی محصول

            //انتساب ویژگی برای محصول
            AddDetailAssignProperty,                      //انتساب ویژگی  
            DeleteDetailAssignProperty,                   //حذف ویژگی انتساب داده شده 
            ChangeSortDetailAssignProperty,               //تغییر اولویت ویژگی انتساب داده شده

            //محصولات
            AddProduct,                                   //ثبت محصول
            EditProduct,                                  //ویرایش محصول
            DeleteProduct,                                //حذف محصول
            //اکسسوری برای محصول
            AddAccessoryProduct,                          // انتساب اکسسوری به محصول
            EditAccessoryProduct,                         //ویرایش اکسسوری از محصول
            DeleteAccessoryProduct,                       //حذف اکسسوری از محصول

            //ویژگی فنی برای محصول
            AddDetailTechnicalProperty,                   // انتساب ویژگی فنی به محصول
            EditDetailTechnicalProperty,                  //ویرایش ویژگی فنی از محصول
            DeleteDetailTechnicalProperty,                //حذف ویژگی فنی از محصول

            //ویژگی ظاهری برای محصول
            AddDefineDetailProduct,                        // ثبت ویژگی ظاهری برای محصول
            EditDefineDetailProduct,                          // ویرایش ویژگی ظاهری به محصول  
            DeleteDefineDetailProduct,                        // حذف ویژگی ظاهری از محصول

            //فایل ضمیمه-صفحه محصولات
            AddAttchForProduct,                           //ثبت فایل  برای محصول
            EditAttchForProduct,                          //ویرایش فایل  برای محصول
            DeleteAttchForProduct,                        //حذف فایل  از محصول
            SelectOldAttchForProduct,                     //انتخاب از فایل های قدیمی برای محصول


//menu = ProductProperty.aspx
            AddProperty,                           //ثبت ویژگی ظاهری 
            EditProperty,                          //ویرایش ویژگی ظاهری
            DeleteProperty,                        //حذف ویژگی ظاهری


//menu = ProductTechnicalProperty.aspx
            AddTechnicalProperty,                  //ثبت ویژگی فنی
            EditTechnicalProperty,                 //ویرایش ویژگی فنی
            DeleteTechnicalProperty,               //حذف ویژگی فنی

//menu = Accessory.aspx
            AddAccessory,                                 //ثبت اکسسوری
            EditAccessory,                                 //ویرایش اکسسوری


//menu = ProductFamily.aspx
            AddProductFamily,                       //ثبت خانواده محصول
            EditProductFamily,                 //ویرایش خانواده محصول
            DeleteProductFamily               //حذف خانواده محصول
        }
    }
}