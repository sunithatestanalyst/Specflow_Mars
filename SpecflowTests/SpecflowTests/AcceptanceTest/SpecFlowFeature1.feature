Feature: SpecFlowFeature1
	In order to update my profile 
	As a skill trader
	I want to add my skills 

	Background: Moving to Profile page
	Given i am in Profile page

	#Adding language
@mytag
Scenario Outline: Check if user could able to add a language 
	Given I clicked on  Add New button under language page
	When I add a language<LanguageAdded> and its level<LanguageLevel>
	Then that language should be displayed under language listings
	Examples: 
	| LanguageAdded | LanguageLevel  |
	| Japanese        | Fluent       |
	| Swedish       | Basic          |
	
		
Scenario Outline: Check if user could able to edit a language
   Given I clicked on Edit button of a language added <language>
   When I update language<editlang> and its level<editlevel>
   Then Language is edited propertly and displayed under listings
	Examples: 
	| language | editlang | editlevel |
	| Frenchss    | korean   | Fluent    |
	

Scenario Outline: check if user is able to delete a language
Given I click on Delete button of a language<LangToDelete>
Then language is deleted is from the listings
Examples: 
| LangToDelete |
| korean     |


Scenario: Check if user is able to add same language more than once
Given I clicked on Add New button under language tab
When I add language that is already present and level
Then Language should not be listed under listings

#Adding Technical skills

Scenario:check if user is able to add technical skills
Given I clicked on Add new button under Skills tab
When I add my technical skills and its level
Then Added skills should be displayed under listings

Scenario:check if user is able to edit technical skills
Given I clicked on edit button of a skills under Skills tab
When I edit my technical skills and its level
Then Edited skills should be displayed under listings

Scenario:check if user is able to delete technical skills
Given I clicked on Delete button of a skills under Skills tab
Then  skills should be deleted from the listings

#Adding Education
Scenario:check if user is able to add their education 
Given I clicked on Add New button under Education tab
When i add my degree,university,major, year of graduation
Then Education details should be displayed under listings

Scenario: Check if user is able to edit their education
Given I clicked on edit button of a degree under Education tab
When i edit my degree,university,major, year of graduation
Then  edited education details should be displayed under listings

Scenario: Check if user is able to delete their education
Given I clicked on delete button of a degree under Education tab
Then   education details should be removed under listings

#Certifications
Scenario:Check if user is able to their awards and honors
Given I clicked on Add New button under certification tab
When i add my Award/certification,certification from,year
Then Awards  should be displayed under listings

Scenario: Check if user is edit awards and honors
Given I clicked on edit button of a award under certification tab
When i edit my Award/certification,certification from,year
Then Edited Awards  should be displayed under listings

Scenario: Check if user is delete awards and honors
Given I clicked on delete button of a award under certification tab
Then Deleted awards  should not be displayed in the listings