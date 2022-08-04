@AddItemToCartFeatures @NoHeadless
Feature: AddItemToBasket
	This will add a product to the shopping cart

@Smoke
Scenario: Adding a product to the shopping cart
	Given I have navigated to the takealot landingpage 
	And I have searched for a product
	When I select add to cart 
	Then The product will be added to the cart


