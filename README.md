Domain-Driven-Design-Example C#
============================

**Repository objective:** To supplement blog articles on DDD (see below) and create easy to follow Domain-driven design repository that makes sense (if it still makes no sense then please do let me know). 

Please do note that this is not a final solution and should be used for educational purposes only. 

###Articles

[Applied Domain-Driven Design (DDD), Part 1 - Basics](http://www.zankavtaskin.com/2013/09/applied-domain-driven-design-ddd-part-1.html)

[Applied Domain-Driven Design (DDD), Part 2 - Domain events](http://www.zankavtaskin.com/2013/09/applied-domain-driven-design-ddd-part-2.html)

[Applied Domain-Driven Design (DDD), Part 3 - Specification Pattern](http://www.zankavtaskin.com/2013/10/applied-domain-driven-design-ddd-part-3.html)

[Applied Domain-Driven Design (DDD), Part 4 - Infrastructure](http://www.zankavtaskin.com/2013/11/applied-domain-driven-design-ddd-part-4_16.html)

[Applied Domain-Driven Design (DDD), Part 5 - Domain Service](http://www.zankavtaskin.com/2013/11/applied-domain-driven-design-ddd-part-4.html)

[Applied Domain-Driven Design (DDD), Part 6 - Application Services](http://www.zankavtaskin.com/2013/11/applied-domain-driven-design-ddd-part-6.html)

![alt tag](http://1.bp.blogspot.com/-f9QYYWLc1Uk/UoKzpDHYkkI/AAAAAAAACA4/OD1bq9MLYFY/s1600/DDD_png_pure.png)

###Structure

* eCommerce.WebService
* eCommerce
     * ApplicationLayer
          * Domain Area
               * Dto
               * Interface
               * Implementation 
     * DomainModelLayer
          * Domain Area
               * Entity
               * Enum
               * Spec
               * Value
               * Event
     * InfrastructureLayer

###API

To keep things simple I have created in memory repository with few saved items. To start using simply download, open solution and run. 



####Customer

Customer already exists (John Smith, with ID: 5D5020DA-47DF-4C82-A722-C8DEAF06AE23).

But if you would like to add my customers here are urls that you can call.

```
api/customer/add?FirstName=john2&LastName=smith2&Email=john2.smith2@microsoft.com

api/customer/Getbyid/5D5020DA-47DF-4C82-A722-C8DEAF06AE23

api/customer/IsEmailAvailable?email=smith.john@microsoft.com

api/customer/RemoveById/5D5020DA-47DF-4C82-A722-C8DEAF06AE23

api/customer/update?id=5D5020DA-47DF-4C82-A722-C8DEAF06AE23&Email=smith.john@microsoft.com
```

####Product

Product already exists (iPhone, with ID: 65D03D7E-E41A-49BC-8680-DC942BABD10A).

But if you would like to add more products here are urls that you can call.

```
api/product/add?name=iPhone5&quantity=6&cost=422&productcodeid=B2773EBF-CD0C-4F31-83E2-691973E32531

api/product/get/65D03D7E-E41A-49BC-8680-DC942BABD10A
 ```    
 
####Cart

Customer and product already exists, so feel free just to call these urls.
```
/api/cart/add?customerid=5D5020DA-47DF-4C82-A722-C8DEAF06AE23&productid=65D03D7E-E41A-49BC-8680-DC942BABD10A&quantity=1

/api/cart/getbyid?customerid=5D5020DA-47DF-4C82-A722-C8DEAF06AE23

/api/cart/remove?customerid=5D5020DA-47DF-4C82-A722-C8DEAF06AE23&productid=&Quantity=1

/api/cart/checkout?customerid=5D5020DA-47DF-4C82-A722-C8DEAF06AE23
```

