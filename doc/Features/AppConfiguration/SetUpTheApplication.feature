Feature: Set up the application

As demo person
I want to have my Jestream app working on my device
So that I want to use Jestream app to demo mobile story


Scenario: Connect to Jetstream site
Given Jetstream site is available from device
And Annonimous read access is granted for Jetstream site
When demo person set Jetstream site url in the app settings
Then app is connected to Jetstream site

