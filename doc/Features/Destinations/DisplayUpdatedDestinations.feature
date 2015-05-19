Feature: Display updated destinations in the app

As Content Author (Caroline)
I want to see updated destinations in the app
So that I do not need to contact IT specialists to update content in the app


Scenario: Create Destination
Given destination items in Jetstream backend
When Caroline add "destination1" item to Jetstream backend
And Caroline publish "destination1" item
And Caroline press refresh button on the destination screen
Then "destination1" appears in the app


Scenario: Edit Destination
Given "destination1" item exists in Jetstream backend
When Caroline edits "destination1" item "Image" field in Jetstream backend
And publish "destination1" item
And Caroline press refresh button on the destination screen
Then updated "destination1" image is shown in the app


Scenario: Delete destination
Given "destination1" item exists in Jetstream backend
When Caroline delete "destination1" item in Jetstream backend
And publish Destinations folder item
And Caroline press refresh button on the destination screen
Then "destination1" is disappears in the app

