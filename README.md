# Vendr Product Reviews :star:

Basic functionality to view and manage reviews in Vendr.

Insert the following partial on the product page:

```
@Html.Partial("ProductReviews", new ViewDataDictionary
{
    { "storeId", store.Id },
    { "productReference", Model.GetProductReference() }
})
```

## TODO

- [x] Add example of basic review form on product page.
- [x] Extract reviews for product and calculated average score.
- [x] Add tree nodes for each store in backoffice.
- [x] Add paged list of reviews in backoffice with filter options.
- [ ] Search reviews in specific properties.
- [x] Add page to view and edit some properties on review.
- [x] Allow to delete review(s).
- [ ] Allow to change status og review(s).
- [ ] Change review UpdateDate on save.
