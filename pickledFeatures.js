jsonPWrapper ({
  "Features": [
    {
      "RelativeFolder": "04 Other screens\\OnlineCheckinScreen.feature",
      "Feature": {
        "Name": "Online checkin screen",
        "Description": "\r\n> *As app user (Victoria)*  \r\n> *I want to checkin on my flight from the app*  \r\n> *So that I do nt need to spend time at registration desk*  \r\n\r\n[#56638](http://tfs4dk1.dk.sitecore.net/tfs/PD-Products-01/Products/_workitems/edit/56638)\r\n\r\n<h2>Description</h2>\r\nFor the demo purposes only menu item is left without working functionality behind that.  \r\n*Screen:* Android / iOS  \r\n![alt text](img/Feature_images/Check_in_Android.jpg)\r\n![alt text](img/Feature_images/Check_in_iOS.jpg)",
        "FeatureElements": [
          {
            "Name": "Display Online checkin screen",
            "Description": "",
            "Steps": [
              {
                "Keyword": "Given",
                "NativeKeyword": "Given ",
                "Name": "Destination screen opened in the app"
              },
              {
                "Keyword": "When",
                "NativeKeyword": "When ",
                "Name": "Victoria select \"Online checkin\" in the app menu"
              },
              {
                "Keyword": "Then",
                "NativeKeyword": "Then ",
                "Name": "\"Online checkin\" screen appears with the Title only"
              },
              {
                "Keyword": "And",
                "NativeKeyword": "And ",
                "Name": "no information is loaded from the Jetstream instance"
              }
            ],
            "Tags": [],
            "Result": {
              "WasExecuted": false,
              "WasSuccessful": false
            }
          }
        ],
        "Result": {
          "WasExecuted": false,
          "WasSuccessful": false
        },
        "Tags": []
      },
      "Result": {
        "WasExecuted": false,
        "WasSuccessful": false
      }
    },
    {
      "RelativeFolder": "04 Other screens\\FlightStatusScreen.feature",
      "Feature": {
        "Name": "Flight status screen",
        "Description": "\r\n> *As app user (Victoria)*  \r\n> *I want to get the flight status from the app*  \r\n> *So that I am aware about any flight changes*  \r\n\r\n[#56637](http://tfs4dk1.dk.sitecore.net/tfs/PD-Products-01/Products/_workitems/edit/56637)\r\n\r\n<h2>Description</h2>\r\nFor the demo purposes only menu item is left without working functionality behind that.  \r\n*Screen:* Android / iOS  \r\n![alt text](img/Feature_images/Flight_status_Android.jpg)\r\n![alt text](img/Feature_images/Flight_status_iOS.jpg)",
        "FeatureElements": [
          {
            "Name": "Display Flight status screen",
            "Description": "",
            "Steps": [
              {
                "Keyword": "Given",
                "NativeKeyword": "Given ",
                "Name": "Destination screen opened in the app"
              },
              {
                "Keyword": "When",
                "NativeKeyword": "When ",
                "Name": "Victoria select \"Flight status\" in the app menu"
              },
              {
                "Keyword": "Then",
                "NativeKeyword": "Then ",
                "Name": "\"Flight status\" screen appears with the Title only"
              },
              {
                "Keyword": "And",
                "NativeKeyword": "And ",
                "Name": "no information is loaded from the Jetstream instance"
              }
            ],
            "Tags": [],
            "Result": {
              "WasExecuted": false,
              "WasSuccessful": false
            }
          }
        ],
        "Result": {
          "WasExecuted": false,
          "WasSuccessful": false
        },
        "Tags": []
      },
      "Result": {
        "WasExecuted": false,
        "WasSuccessful": false
      }
    },
    {
      "RelativeFolder": "04 Other screens\\AboutScreen.feature",
      "Feature": {
        "Name": "About screen",
        "Description": "\r\n>*As Content Author ([Caroline](http://intranet/Project-Rooms/UX/Sitecore-personas/Marketing-personas/Caroline.aspx))*  \r\n>*I want to see actual content from another part of my website in the app*  \r\n>*So that I can keep all in app content up to date from backend only*  \r\n\r\n[#56096](http://tfs4dk1.dk.sitecore.net/tfs/PD-Products-01/Products/_workitems/edit/56096)\r\n\r\n<h2>Description</h2>\r\nIn the \"About\" section, user can view the Jetstream About information.  \r\n*Screen:* Android / iOS  \r\n![alt text](img/Feature_images/About_Android.jpg)\r\n![alt text](img/Feature_images/About_iOS.jpg)  \r\n\r\n**Points of interest**  \r\n1. The screen contains **Title**, **Summary** and **Body** that are loaded from the Jetstream About item.  \r\n2. The **arrow icon** in the top left corner allows the User to open another app screen.",
        "FeatureElements": [
          {
            "Name": "Display About item info",
            "Description": "",
            "Steps": [
              {
                "Keyword": "Given",
                "NativeKeyword": "Given ",
                "Name": "Destination screen opened in the app"
              },
              {
                "Keyword": "When",
                "NativeKeyword": "When ",
                "Name": "Caroline select \"About\" in the app menu"
              },
              {
                "Keyword": "Then",
                "NativeKeyword": "Then ",
                "Name": "About screen appears with \"About\" item info"
              }
            ],
            "Tags": [],
            "Result": {
              "WasExecuted": false,
              "WasSuccessful": false
            }
          },
          {
            "Name": "Display updated About item info",
            "Description": "",
            "Steps": [
              {
                "Keyword": "Given",
                "NativeKeyword": "Given ",
                "Name": "About screen with \"About\" item info in the app"
              },
              {
                "Keyword": "When",
                "NativeKeyword": "When ",
                "Name": "Caroline publish new version of About item with updated info in Jetstream backend"
              },
              {
                "Keyword": "And",
                "NativeKeyword": "And ",
                "Name": "Caroline select \"About\" in app menu"
              },
              {
                "Keyword": "Then",
                "NativeKeyword": "Then ",
                "Name": "About screen appears with updated \"About\" item info"
              }
            ],
            "Tags": [],
            "Result": {
              "WasExecuted": false,
              "WasSuccessful": false
            }
          }
        ],
        "Result": {
          "WasExecuted": false,
          "WasSuccessful": false
        },
        "Tags": []
      },
      "Result": {
        "WasExecuted": false,
        "WasSuccessful": false
      }
    },
    {
      "RelativeFolder": "03 Destinations\\DisplayUpdatedDestinations.feature",
      "Feature": {
        "Name": "Display updated destinations in the app",
        "Description": "\r\n> *As Content Author ([Caroline](http://intranet/Project-Rooms/UX/Sitecore-personas/Marketing-personas/Caroline.aspx))*  \r\n> *I want to see updated destinations in the app*  \r\n> *So that I do not need to contact IT specialists to update content in the app*\r\n\r\n<h2>Description</h2>\r\nDestinations are used to show how the app could reuse the content from the website. Content mangers could create, update and remove the content items in the platform and see the changes in the app. The changes are applied after publishing, as it makes the content managers just do their job.",
        "FeatureElements": [
          {
            "Name": "Create Destination",
            "Description": "",
            "Steps": [
              {
                "Keyword": "Given",
                "NativeKeyword": "Given ",
                "Name": "destination items in Jetstream backend"
              },
              {
                "Keyword": "When",
                "NativeKeyword": "When ",
                "Name": "Caroline adds \"destination1\" item to Jetstream backend"
              },
              {
                "Keyword": "And",
                "NativeKeyword": "And ",
                "Name": "Caroline publishes \"destination1\" item"
              },
              {
                "Keyword": "And",
                "NativeKeyword": "And ",
                "Name": "Caroline presses refresh button on the destination screen"
              },
              {
                "Keyword": "Then",
                "NativeKeyword": "Then ",
                "Name": "\"destination1\" appears in the app"
              }
            ],
            "Tags": [],
            "Result": {
              "WasExecuted": false,
              "WasSuccessful": false
            }
          },
          {
            "Name": "Edit Destination",
            "Description": "",
            "Steps": [
              {
                "Keyword": "Given",
                "NativeKeyword": "Given ",
                "Name": "\"destination1\" item exists in Jetstream backend"
              },
              {
                "Keyword": "When",
                "NativeKeyword": "When ",
                "Name": "Caroline edits \"destination1\" item \"Image\" field in Jetstream backend"
              },
              {
                "Keyword": "And",
                "NativeKeyword": "And ",
                "Name": "publish \"destination1\" item"
              },
              {
                "Keyword": "And",
                "NativeKeyword": "And ",
                "Name": "Caroline presses refresh button on the destination screen"
              },
              {
                "Keyword": "Then",
                "NativeKeyword": "Then ",
                "Name": "updated \"destination1\" image is shown in the app"
              }
            ],
            "Tags": [],
            "Result": {
              "WasExecuted": false,
              "WasSuccessful": false
            }
          },
          {
            "Name": "Delete Destination",
            "Description": "",
            "Steps": [
              {
                "Keyword": "Given",
                "NativeKeyword": "Given ",
                "Name": "\"destination1\" item exists in Jetstream backend"
              },
              {
                "Keyword": "When",
                "NativeKeyword": "When ",
                "Name": "Caroline deletes \"destination1\" item in Jetstream backend"
              },
              {
                "Keyword": "And",
                "NativeKeyword": "And ",
                "Name": "publish Destinations folder item"
              },
              {
                "Keyword": "And",
                "NativeKeyword": "And ",
                "Name": "Caroline presses refresh button on the destination screen"
              },
              {
                "Keyword": "Then",
                "NativeKeyword": "Then ",
                "Name": "\"destination1\" is disappears in the app"
              }
            ],
            "Tags": [],
            "Result": {
              "WasExecuted": false,
              "WasSuccessful": false
            }
          }
        ],
        "Result": {
          "WasExecuted": false,
          "WasSuccessful": false
        },
        "Tags": []
      },
      "Result": {
        "WasExecuted": false,
        "WasSuccessful": false
      }
    },
    {
      "RelativeFolder": "03 Destinations\\DisplayDestinations.feature",
      "Feature": {
        "Name": "Display destinations",
        "Description": "\r\n> *As Content Author ([Caroline](http://intranet/Project-Rooms/UX/Sitecore-personas/Marketing-personas/Caroline.aspx))*  \r\n> *I want to see Jetstream destinations in the app*  \r\n> *So that I can reuse already created Jetstream content for the app*  \r\n\r\n[#56096](http://tfs4dk1.dk.sitecore.net/tfs/PD-Products-01/Products/_workitems/edit/56096),\r\n[#56080](http://tfs4dk1.dk.sitecore.net/tfs/PD-Products-01/Products/_workitems/edit/56080),\r\n[#56077](http://tfs4dk1.dk.sitecore.net/tfs/PD-Products-01/Products/_workitems/edit/56077)\r\n\r\n<h2>Description</h2>\r\n<h3>All destinations</h3>\r\nThe initial screen of the app is organized as the world map. Jetstream destinations appear on the map as map markers that the user can tap on to view the details page of a specific destination.  \r\n*Screen:* Android / iOS  \r\n![alt text](img/Feature_images/Destinations_MapWithCarousel_Android.jpg)\r\n![alt text](img/Feature_images/Destinations_MapWithCarousel_iOS.jpg)  \r\n\r\n\r\n**Points of interest**  \r\n1. Screen **Title** and **Logo** (at the top left corner of the app) are loaded from the Jetstream.    \r\n2. **Images** for map markers are loaded from the Jetstream destinations.  \r\n3. Markers use the **latitude/longitude** coordinates from the destinations content.  \r\n4. User can tap on a \"Destination\" marker to view a destination details.  \r\n\r\n<h3>Destinations carousel</h3>\r\nThere is a destinations carousel at the bottom of the map.  \r\n![alt text](img/Feature_images/Destinations_MapWithCarousel_Android.jpg)\r\n![alt text](img/Feature_images/Destinations_MapWithCarousel_iOS.jpg)  \r\n\r\n**Points of interest**  \r\n1. Destination elements for carousel are loaded from the Jetstream instance.  \r\n2. **Name** and **Image** for a destinations element are loaded from the Jetstream instance.  \r\n3. User can tap on a destination element to view the destination details.  \r\n\r\n<h3>Destination details</h3>\r\nDestination details is opened on a separate screen.  \r\n*Screen:* Android / iOS  \r\n![alt text](img/Feature_images/Destinations_Details_Android.jpg)\r\n![alt text](img/Feature_images/Destinations_Details_iOS.jpg)  \r\n\r\n**Points of interest**  \r\n1. User can view an image of the destination, as well as information about it.  \r\n2. **Description** for destination is loaded from the Jetstream instance.  \r\n3. **Images** are loaded from attraction items from the Jetstream instance.  \r\n4. The \"Back\" button allows the User to open the destinations map screen.",
        "FeatureElements": [
          {
            "Name": "Display all destinations from Jetstream on a map",
            "Description": "",
            "Steps": [
              {
                "Keyword": "Given",
                "NativeKeyword": "Given ",
                "Name": "destination items in Jetstream backend"
              },
              {
                "Keyword": "When",
                "NativeKeyword": "When ",
                "Name": "Caroline opens the app"
              },
              {
                "Keyword": "Then",
                "NativeKeyword": "Then ",
                "Name": "all destinations are shown on the map"
              }
            ],
            "Tags": [],
            "Result": {
              "WasExecuted": false,
              "WasSuccessful": false
            }
          },
          {
            "Name": "Group destinations which overlay each other",
            "Description": "",
            "Steps": [
              {
                "Keyword": "Given",
                "NativeKeyword": "Given ",
                "Name": "destination items in Jetstream backend"
              },
              {
                "Keyword": "And",
                "NativeKeyword": "And ",
                "Name": "Caroline opens the app"
              },
              {
                "Keyword": "And",
                "NativeKeyword": "And ",
                "Name": "Caroline zoom out the map"
              },
              {
                "Keyword": "When",
                "NativeKeyword": "When ",
                "Name": "Destinations Paris, London and Amsterdam are too close to one another to display separately"
              },
              {
                "Keyword": "Then",
                "NativeKeyword": "Then ",
                "Name": "Paris, London and Amsterdam grouped to one marker"
              },
              {
                "Keyword": "And",
                "NativeKeyword": "And ",
                "Name": "Marker contains picture of one of them"
              },
              {
                "Keyword": "And",
                "NativeKeyword": "And ",
                "Name": "Marker contains label with text \"3\" on it"
              }
            ],
            "Tags": [],
            "Result": {
              "WasExecuted": false,
              "WasSuccessful": false
            }
          },
          {
            "Name": "Display all destination on a map",
            "Description": "",
            "Steps": [
              {
                "Keyword": "Given",
                "NativeKeyword": "Given ",
                "Name": "destination items in Jetstream backend"
              },
              {
                "Keyword": "When",
                "NativeKeyword": "When ",
                "Name": "Caroline opens the app"
              },
              {
                "Keyword": "Then",
                "NativeKeyword": "Then ",
                "Name": "all destinations are shown at the bottom of the map"
              },
              {
                "Keyword": "And",
                "NativeKeyword": "And ",
                "Name": "destinations are sorted alphabetically"
              }
            ],
            "Tags": [],
            "Result": {
              "WasExecuted": false,
              "WasSuccessful": false
            }
          },
          {
            "Name": "Display Destination details",
            "Description": "",
            "Steps": [
              {
                "Keyword": "Given",
                "NativeKeyword": "Given ",
                "Name": "\"London\" destination is shown in the app"
              },
              {
                "Keyword": "When",
                "NativeKeyword": "When ",
                "Name": "Caroline tap \"London\" on the map"
              },
              {
                "Keyword": "Then",
                "NativeKeyword": "Then ",
                "Name": "details screen appears with \"London\" image and description"
              }
            ],
            "Tags": [],
            "Result": {
              "WasExecuted": false,
              "WasSuccessful": false
            }
          }
        ],
        "Result": {
          "WasExecuted": false,
          "WasSuccessful": false
        },
        "Tags": []
      },
      "Result": {
        "WasExecuted": false,
        "WasSuccessful": false
      }
    },
    {
      "RelativeFolder": "02 Menu\\AppMenu.feature",
      "Feature": {
        "Name": "App menu",
        "Description": "\r\n> *As app user ([Victoria](http://intranet/Project-Rooms/UX/Sitecore-personas/Persona-Shopper/Victoria.aspx))*  \r\n> *I want to have the app menu*  \r\n> *So that I can navigate in the app*  \r\n\r\n<h2>Description</h2>\r\nThe app menu contains the list of options the user can navigate to.  \r\n*Screen:* Android / iOS  \r\n![alt text](img/Feature_images/AppMenu_Android.jpg)\r\n![alt text](img/Feature_images/AppMenu_iOS.jpg)  \r\n\r\n**Points of interest**  \r\n1. User can tap on menu icon in the top left corner of the app to open the menu panel.  \r\n2. Menu panel is appeared from left to right.  \r\n3. User can tap on **\"About\"** to open an about screen.  \r\n4. User can tap on **\"Destination\"** to open map with destinations.  \r\n5. User can tap on **\"Flight status\"** to open a flight status screen (empty in this version).  \r\n6. User can tap on **\"Online checkin\"** to open a online checkin screen (empty in this version).  \r\n7. User can tap on **\"Settings\"** to open the app settings screen.",
        "FeatureElements": [
          {
            "Name": "Display menu",
            "Description": "",
            "Steps": [
              {
                "Keyword": "Given",
                "NativeKeyword": "Given ",
                "Name": "start screen of the app is displayed"
              },
              {
                "Keyword": "When",
                "NativeKeyword": "When ",
                "Name": "user tap the menu icon"
              },
              {
                "Keyword": "Then",
                "NativeKeyword": "Then ",
                "Name": "the menu is displayed"
              }
            ],
            "Tags": [],
            "Result": {
              "WasExecuted": false,
              "WasSuccessful": false
            }
          }
        ],
        "Result": {
          "WasExecuted": false,
          "WasSuccessful": false
        },
        "Tags": []
      },
      "Result": {
        "WasExecuted": false,
        "WasSuccessful": false
      }
    },
    {
      "RelativeFolder": "01 AppConfiguration\\SetUpTheApplication.feature",
      "Feature": {
        "Name": "Set up the application",
        "Description": "\r\n> *As demo person*  \r\n> *I want to have my Jestream app working on my device*  \r\n> *So that I want to use Jestream app to demo mobile story*  \r\n\r\n[#56061](http://tfs4dk1.dk.sitecore.net/tfs/PD-Products-01/Products/_workitems/edit/56061)\r\n\r\n<h2>Description</h2> \r\nTo show content from a Jetstream instance the app need to be \"connected\" to that Jetstream demo instance.  \r\n*Screen:* Android / iOS  \r\n![alt text](img/Feature_images/01AppConfiguration_ConnectToJetstreamInstance_Android.jpg)\r\n![alt text](img/Feature_images/01AppConfiguration_ConnectToJetstreamInstance_iOS.jpg)\r\n\r\n\r\n**Points of interest**  \r\n1. \"Settings\" screen (for iOS) or dialogue (for Android) could be opened from the app menu.  \r\n2. Just site name should be entered to connect Jetstream demo instance.  \r\n2.1. iOS: To apply new URL just go to another menu option in the app menu.  \r\n2.2. Android: To apply new URL just click button \"OK\" in the settings dialogue.",
        "FeatureElements": [
          {
            "Name": "Connect to Jetstream instance",
            "Description": "",
            "Steps": [
              {
                "Keyword": "Given",
                "NativeKeyword": "Given ",
                "Name": "Jetstream1 instance is available from device"
              },
              {
                "Keyword": "And",
                "NativeKeyword": "And ",
                "Name": "Anonymous read access is granted for Jetstream1 instance"
              },
              {
                "Keyword": "When",
                "NativeKeyword": "When ",
                "Name": "demo person set Jetstream1 instance url in the app settings"
              },
              {
                "Keyword": "Then",
                "NativeKeyword": "Then ",
                "Name": "app is connected to Jetstream1 instance"
              },
              {
                "Keyword": "And",
                "NativeKeyword": "And ",
                "Name": "content form Jetstream1 instance displayed in the app"
              }
            ],
            "Tags": [],
            "Result": {
              "WasExecuted": false,
              "WasSuccessful": false
            }
          },
          {
            "Name": "Connect to another Jetstream instance",
            "Description": "",
            "Steps": [
              {
                "Keyword": "Given",
                "NativeKeyword": "Given ",
                "Name": "app connected to Jetstream1 instance"
              },
              {
                "Keyword": "When",
                "NativeKeyword": "When ",
                "Name": "demo person set Jetstream2 instance url in the app settings"
              },
              {
                "Keyword": "Then",
                "NativeKeyword": "Then ",
                "Name": "app is connected to Jetstream2 instance"
              },
              {
                "Keyword": "And",
                "NativeKeyword": "And ",
                "Name": "content from Jetstream1 instance disappeared"
              },
              {
                "Keyword": "And",
                "NativeKeyword": "And ",
                "Name": "content form Jetstream2 instance displayed in the app"
              }
            ],
            "Tags": [],
            "Result": {
              "WasExecuted": false,
              "WasSuccessful": false
            }
          }
        ],
        "Result": {
          "WasExecuted": false,
          "WasSuccessful": false
        },
        "Tags": []
      },
      "Result": {
        "WasExecuted": false,
        "WasSuccessful": false
      }
    },
    {
      "RelativeFolder": "01 AppConfiguration\\SetUpDemoContent.feature",
      "Feature": {
        "Name": "Set up demo content",
        "Description": "\r\n> *As demo person*  \r\n> *I want to have my Jestream site configured for using in the app*  \r\n> *So that I want to use Jestream app with content from the site to demo mobile story*  \r\n\r\n<h2>Description</h2> \r\nTo use the data from Jetstream demo instance in the app the instance should be configured properly:\r\n1. Destinations should have nice images and setted up latitude / longitude fields;\r\n2. Item Web API should be configured to allow read access on website for anonymous user.  \r\n\r\nTo configure the Jetstream site in that way you should simply install \"Jetstream mobile app package-1.0.zip\" Sitecore package:\r\n![alt text](img/Feature_images/install_demo_package.jpg)",
        "FeatureElements": [],
        "Result": {
          "WasExecuted": false,
          "WasSuccessful": false
        },
        "Tags": []
      },
      "Result": {
        "WasExecuted": false,
        "WasSuccessful": false
      }
    },
    {
      "RelativeFolder": "Home.feature",
      "Feature": {
        "Name": "Jetstream mobile demo app",
        "Description": "\r\n<h2>Idea</h2>\r\nBuild a native app to assist Sales to demo a mobile story.  \r\n\r\n<h2>Value</h2>\r\nProject will add demo “wow” factor in direct comparison to similar demos by Adobe.  \r\nSales will get easy to install/use/demo application for iOS and Android platforms.  \r\nApp will deliver a well known content from Jetstream and will be integrated with Jetstream scenarios.  \r\n\r\n<h2>Primary objectives</h2>\r\n1. Deliver up-to-date content from Jetstream demo site.  \r\n2. Build mobile apps for iOS and Android platforms.  \r\n3. Design apps for Tablets only.  \r\n4. Use Mobile SDK for Xamarin.",
        "FeatureElements": [],
        "Result": {
          "WasExecuted": false,
          "WasSuccessful": false
        },
        "Tags": []
      },
      "Result": {
        "WasExecuted": false,
        "WasSuccessful": false
      }
    }
  ],
  "Configuration": {
    "SutName": "Jetstream",
    "SutVersion": "1.0",
    "GeneratedOn": "14 July 2015 12:47:31"
  }
});