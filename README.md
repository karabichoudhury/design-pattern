# design-pattern
This project outlines the creation of a service which gives api's to perform the CRUD operations. Besides this, it also has some api's to handle specific requests of pulling data based on some filters
Some basics regarding the methods
Add() - When adding primary category specify model as "PC".For Secondary category specify model as "SC" and assumption is made that primary category id will be known while caaling the api. Similary for product , model is "PR" and secondary category needs to be passed.
GetScheme () - Sepcify the id of the in the inputs specified
GetSchemeForProducts()- input is a list of a class, whose member is product id and quantity ordered.
Update() - is made based on shortcode(since id might vary while setting up...ideal condition)

The design patterns which followed while creating this were generic repository, template, factory,adapter( incomplete)
While i had done the database transactions through ADO, since i was using generic repository ,i had started to do the DAL layer with Entity framework.But it is still pending for completion


