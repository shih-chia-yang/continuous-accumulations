# mvc-pattern
- the model-view-controller architectural pattern separates an application into three main groups of components. these components are : models , views ,and controller
- the model dose not depend on the view or the controller; both view and the controller depend on the model

- model 
1. these are the classes that represent the domain model.
2. strongly-typed views typically use ViewModel types designed to contain the data to display on that view.
3. the controller creates and populates these ViewModel instance from the model. 
4. models run on the server site

- controller
1. controller is a class that manages the interactions and data moves between the views and models
2. controller is the initial entry point in mvc pattern
3. controller is responsible for selection which model to work and which view to render

- view
1. these are dynamically generated HTML pages from templates
2. there should be minimal logic within views
3. creating view template run on the server. any javascript code and HTML code will run on the browser
