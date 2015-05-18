Feature: Demonstrate native app experience with real-time content syndication and content editing

As Content Author (Caroline)
I want to edit content in SXP and see that changes in native app
So that I do not need to contact IT specialists to support up to date content in native app

Scenario: Display content from SXP
Given cities items in Jetstream backend
When Caroline open native app
Then all cities are shown

Scenario: Support real-time content
Given cities items in Jetstream backend
When Caroline add new city to the Jetstream backend
And publish city item
Then new city is shown in the native app

Scenario: Support content adjustments
Given cities items in Jetstream backend
When Caroline change city field in the Jetstream backend
And publish city item
Then updated city field is shown in the native app
