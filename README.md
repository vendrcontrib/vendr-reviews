# Vendr Product Reviews :star:

Basic functionality to view and manage reviews in Vendr.

Insert the following partial on the product page:

```
@Html.Partial("ProductReviews", new ViewDataDictionary{ {"productReference", Model.GetProductReference() } })
```
