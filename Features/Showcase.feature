Feature: Show case
  As a user of Automation Exercise platform
  I want to be able to register, manage products in cart, and query APIs

@UI @Smoke
Scenario: User registers successfully with valid details
	When I click on Signup/Login link
	Then I'm navigated to 'login' page
	When I enter new unique signup user info
	And I Press on Signup button
	Then I'm navigated to 'signup' page
	And  Unique name and email showed filled up in Account Information
	When I enter new password in Account Information
	And I enter address information
	And I click create account
	Then I should see the 'ACCOUNT CREATED!' confirmation message

#@UI @Cart
#Scenario: Add products to cart and verify cart contents
#	Given I navigate to the home page
#	When I add the first product to the shopping cart
#	And I navigate to the cart page
#	Then the added product should be displayed in the cart with correct details
#
#@UI @Search
#Scenario: Search for a product and verify search results
#	Given I navigate to the "Products" page
#	When I search for product "Dress"
#	Then all displayed products should contain "Dress" in their titles
#
#@API @Products
#Scenario: Retrieve all products list via API
#	Given I send a GET request to "/api/productsList"
#	Then the API response status code should be 200
#	And the response body should contain the list of products
#
#@API @Search
#Scenario: Search product via POST API request
#	Given I send a POST request to "/api/searchProduct" with name "Tshirt"
#	Then the API response status code should be 200
#	And the response body should contain matching "Tshirt" products