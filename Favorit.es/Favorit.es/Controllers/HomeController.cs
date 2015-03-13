using FlickrNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Favorit.es.Models;

namespace Favorit.es.Controllers
{
    public class HomeController : Controller
    {
        #region " PROPERTIES "

        private List<AddFavoriteViewModel> _userFavorites;
        public List<AddFavoriteViewModel> UserFavorites
        {
            get
            {
                //this is called Lazy Loading, its a read only property that propogates itself if it is null either by creating a new instance, or filling it from a datasource like a database, or in our case the Session
                if (_userFavorites == null)
                {
                    //if the variable is null, try to grab it from the Session data store.
                    _userFavorites = (List<AddFavoriteViewModel>)Session["myFavorites"]; //cast the object as a List of AddFavoriteViewModel
                    if (_userFavorites == null)
                    {
                        //if its still null, that means there isn't one in the data store, lets create a new object to populate our variable with
                        _userFavorites = new List<AddFavoriteViewModel>();
                    }
                }
                return _userFavorites;
            }
        }

        #endregion


        /// <summary>
        /// simple method to save the UserFavorites property to the Session data store.  Easier that retyping the line over and over.
        /// </summary>
        private void SaveUserFavorites()
        {
            Session["myFavorites"] = this.UserFavorites;
        }

        /// <summary>
        /// default landing page, displays kitten images
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Index()
        {
            //go to: https://www.flickr.com/services/apps/create/apply/ to get your api key
            //create a new instance of the Flickr API object
            var flickr = new Flickr("a386da5b094d63f3583fd833d126eca2", "0a099df2c5f76240"); 
           
            //create a new object that represents searching for photos
            PhotoSearchOptions options = new PhotoSearchOptions();
            options.Tags = "kittens"; //what we are searching for
            options.PerPage = 25; // results to retrieve
            PhotoCollection photos = flickr.PhotosSearch(options);  //retrieve photos
            return View(photos); //pass the PhotoCollection object to the View

            //more info about how to use the Flickr.NET API @ http://flickrnet.codeplex.com/wikipage?title=Examples
        }

        /// <summary>
        /// The user is searching for photos
        /// </summary>
        /// <param name="searchText"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Index(string searchText)
        {
          //this action is triggered when a user clicks the GO buttton on the main page.  The name attribute in the html input element is matched exactly to the argument name.  

            //go to: https://www.flickr.com/services/apps/create/apply/ to get your api key
            //create a new instance of the Flickr API object
            var flickr = new Flickr("a386da5b094d63f3583fd833d126eca2", "0a099df2c5f76240");

            //create a new object that represents searching for photos
            PhotoSearchOptions options = new PhotoSearchOptions();

            options.Tags = searchText; //what we are searching for
            options.PerPage = 25; // results to retrieve

            PhotoCollection photos = flickr.PhotosSearch(options);  //retrieve photos

            return View(photos); //pass the PhotoCollection object to the View
            
        }

        /// <summary>
        /// The user clicked a photo to save as a favorite
        /// </summary>
        /// <param name="addFavorite">an object representing the photo id, url, and title the user wants to save</param>
        /// <returns></returns>
        public ActionResult Favorite(AddFavoriteViewModel addFavorite)
        {
            //add the AddFavoriteViewModel to our user favorites
            this.UserFavorites.Add(addFavorite);

            //save the UserFavorites
            SaveUserFavorites();

            //kick the user back to the index page.
            return RedirectToAction("index");
        }

        /// <summary>
        /// Shows the user their saved favorites
        /// </summary>
        /// <returns></returns>
        public ActionResult MyFavorites()
        {
            //pass the UserFavorites list to the View
            return View(this.UserFavorites); //goes to GET property and then pass thelist to the view
        }

        /// <summary>
        /// Unfavorite an item
        /// </summary>
        /// <param name="id">the id of the photo to unfavorite</param>
        /// <returns></returns>
        public ActionResult Unfavorite(string id)
        {
            //get the object from the UserFavorites based on the ID
            AddFavoriteViewModel thingToUnfavorite = this.UserFavorites.First(x => x.id == id);

            //remove that object from the UserFavorites
            this.UserFavorites.Remove(thingToUnfavorite);

            //save the user favorites
            SaveUserFavorites();

            //kick the user back to the MyFavorites screen
            return RedirectToAction("MyFavorites");
        }

     
  
   }
 

}