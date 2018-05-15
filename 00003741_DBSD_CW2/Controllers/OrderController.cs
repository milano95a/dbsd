using _00003741_DBSD_CW2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using _00003741_DBSD_CW2.DataAccess;


namespace _00003741_DBSD_CW2.Controllers
{
    public class OrderController : Controller
    {
        public static int NumOfOrderedItems = 0;
        public static int Total = 0;
        public static ProductOrder CurrentItem = null;
        public static List<ProductOrder> OrderList = new List<ProductOrder>();
        private static string ORDER_STATE;

        // GET: Order
        [Authorize]
        public ActionResult Index()
        {
            ViewBag.Count = "Items ordered: " + NumOfOrderedItems +"  /  " + "Total: " + Total;
            List<Products> list = new List<Products>(); 
            DatabaseManager manager = new DatabaseManager();

            if (ORDER_STATE == null)
            {

                list = manager.GetAllProducts();


            }
            else if (ORDER_STATE.Equals("Asc"))
            {
                var asc = from e in manager.GetAllProducts() where e.InStock > 0 orderby e.Name select e;
                return View(asc);

            }
            else if (ORDER_STATE.Equals("Desc"))
            {
                var desc = from e in manager.GetAllProducts() where e.InStock > 0  orderby e.Name descending select e;
                return View(desc);
            }
            else if (ORDER_STATE.Equals("pAsc"))
            {

                var asc = from e in manager.GetAllProducts() where e.InStock > 0  orderby e.Name select e;
                return View(asc);

            }
            else if (ORDER_STATE.Equals("pDesc"))
            {
                var desc = from e in manager.GetAllProducts() where e.InStock > 0  orderby e.Name descending select e;
                return View(desc);
            }

            return View(list);
        }

        [Authorize]
        // Get Item Detail View To Order
        public ActionResult AddToCart(int Id)
        {            
            Products product = new DatabaseManager().GetProductById(Id);

            ProductOrder productOrder = new ProductOrder()
            {
                Id = 0,
                CustomerId = TemporaryData.userId,
                ProductId = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                InStock = product.InStock,
                Quantity = 0
            };
            CurrentItem = productOrder;
            return View(productOrder);
        }

        // Add selected item with quantity to cart
        [HttpPost]
        [Authorize]
        public ActionResult AddToCart(int Quantity, int Id)
        {
            if (Quantity == 0)
            {
                ViewBag.OrderError = "Please enter quantity";
                return View(CurrentItem);
            }
            else if (Quantity >CurrentItem.InStock)
            {
                ViewBag.OrderError = "only " +CurrentItem.InStock + " number of items available in stock";
                return View(CurrentItem);
            }
            else
            {
                CurrentItem.Quantity = Quantity;
                OrderList.Add(CurrentItem);

                Total +=  CurrentItem.Quantity * CurrentItem.Price;
                NumOfOrderedItems += CurrentItem.Quantity;
                return RedirectToAction("Index", "Order");
            }
        }

        // Shows ordered items 
        [Authorize]
        public ActionResult GoToCart()
        {
            ViewBag.Total = "Total: " + Total + "   ";        
            return View(OrderList);
        }

        // removes from ordered items by id
        [Authorize]
        public ActionResult Remove(int Id)
        {
            
            ProductOrder OrderItem = OrderList.First(item => item.Id == Id);

            NumOfOrderedItems -= OrderItem.Quantity;
            Total -= OrderItem.Price * OrderItem.Quantity;

            OrderList.Remove(OrderItem);

            return RedirectToAction("GoToCart");
        }

        [HttpPost]
        // search by product name and manufacturer name
        [Authorize]
        public ActionResult Search(string SearchDescription, string SearchName)
        {
            DatabaseManager manager = new DatabaseManager();


            var searchResultByDescription = from e in manager.GetAllProducts().Where(product => product.Description.ToLower().Contains(SearchDescription.ToLower()) && product.InStock > 0) select e;

            var searchResultByDescriptionAndByName = from e in searchResultByDescription.Where(p => p.Name.ToLower().Contains(SearchName.ToLower()) && p.InStock > 0) select e;

            return View("Index",searchResultByDescriptionAndByName);
        }

        [Authorize]    
        public ActionResult Suggestion()
        {
            DatabaseManager dbManager = new DatabaseManager();
            List<Suggestion> suggestedItems = dbManager.GetAllSuggestion(TemporaryData.userId);
            return View(suggestedItems);
        }

        public ActionResult SortByName()
        {
            if (ORDER_STATE == null || ORDER_STATE.Equals("Desc"))
            {
                ORDER_STATE = "Asc";
            }
            else
            {
                ORDER_STATE = "Desc";
            }

            return RedirectToAction("Index");
        }
        public ActionResult SortByDescription()
        {
            if (ORDER_STATE == null || ORDER_STATE.Equals("pDesc"))
            {
                ORDER_STATE = "pAsc";
            }
            else
            {
                ORDER_STATE = "pDesc";
            }

            return RedirectToAction("Index");
        }
        public ActionResult Pay()
        {
            DatabaseManager dbManager = new DatabaseManager();

            foreach(ProductOrder p in OrderList)
            {
                dbManager.Order(p);                    
            }

            OrderList.Clear();
            Total = 0;
            NumOfOrderedItems = 0;
            return RedirectToAction("Index");
        }

    }
}