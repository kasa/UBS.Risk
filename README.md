Question 2: A new category was created called PEP (politically exposed person). Also, a new bool property
`IsPoliticallyExposed` was created in the ITrade interface. A trade shall be categorized as PEP if
`IsPoliticallyExposed` is true. Describe in at most 1 paragraph what you must do in your design to account
for this new category.

A: To categorize that trade, it's necessary to add an enum item named `PEP` in `TradeCategory`, create a
new strategy class in `UBS.Risk.Categorizer.Strategy` that implements `ITradeCategorizer` and returns 
`TradeCategory.PEP` if `ITrade.IsPoliticallyExposed` is `true` and returns `TradeCategory.NONE` otherwise. 
Finally, when instantiating `TradeCategorizer` in `Program.cs`, add the newly created strategy in the
desired order. If the value for PEP comes from the file, it will be necessary to change `TradeParser.Parse`
to accomodate that change.