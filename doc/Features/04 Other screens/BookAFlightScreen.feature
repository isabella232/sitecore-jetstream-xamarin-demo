Feature: Book a flight screen

> *As app user (Victoria)*  
> *I want to book a flight from the app*  
> *So that I book the interested flights on the go*  

[#56097](http://tfs4dk1.dk.sitecore.net/tfs/PD-Products-01/Products/_workitems/edit/56097)

<h2>Description</h2>
Booking a flight is a the most expected functionality in a real application. For the demo purposes only menu item is left without working functionality behind that.  

Scenario: Display Book a flight screen
Given Destination screen opened in the app  
When Victoria select "Book a flight" in the app menu  
Then "Book a flight" screen appears with the Title only
And no information is loaded from the Jetstream instance
