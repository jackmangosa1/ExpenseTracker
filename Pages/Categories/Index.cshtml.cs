﻿@page
@model ExpenseTracker.Pages.Categories.IndexModel

@{
    ViewData["Title"] = "Index";
}

@if(TempData["Sweet"] != null)
{
   @section Scripts{
       <script>
           $(function () {
               swal({
                   title: "Warning",
                   text: "You have exceeded the 50% of your total budget",
                   icon: 'warning',
                   showCancelButton: false,
                   allowOutsideClick: false,
                   confirmButtonColor: "green",
                   confirmButtonText: "Ok"
               }).then(function () {
               });
           })
       </script>
   } 
}


<h2 class="mb-5">Category Management</h2>
<form method="get">
    <div class="input-group mb-5">
        <input type="text" class="form-control" placeholder="Category Title" aria-label="Recipient's username" aria-describedby="button-addon2" name="SearchTerm" value="@Model.SearchTerm">
        <button class="btn btn-primary" type="submit" id="button-addon2">Search</button>
    </div>
</form>

<p>
    <a asp-page="Create" class="btn btn-primary mb-2">Create Category</a>
</p>
<table class="table">
    <thead>
        <tr>
            <th>
                Category Name
            </th>
            <th>
                Budget Title
            </th>
            <th>
                Allocated Amount
            </th>
            <th>
                Actions
            </th>

        </tr>
    </thead>
    <tbody>
        @foreach (var item in Model.Category)
        {
            <tr>
                <td>
                    @Html.DisplayFor(modelItem => item.CategoryName)
                </td>
                <td>
                    @Html.DisplayFor(modelItem => item.Budget.BudgetName)
                </td>
                <td>
                    @Html.DisplayFor(modelItem => item.AllocatedAmount)
                </td>


                <td>


                    <div class="d-flex">
                        <a asp-page="./Edit" asp-route-id="@item.CategoryID" class="btn btn-primary me-2">Edit</a>

                        <form method="post" asp-page="/Categories/Delete">
                            <input type="hidden" name="id" value="@item.CategoryID" />

                            <!-- Button to trigger the modal -->
                            <button type="button" class="btn btn-danger" data-bs-toggle="modal" data-bs-target="#deleteConfirmationModal-@item.CategoryID">
                                Delete
                            </button>

                            <!-- ... Modal code ... -->
                            <div class="modal" id="deleteConfirmationModal-@item.CategoryID" tabindex="-1" aria-labelledby="deleteConfirmationModalLabel" aria-hidden="true">
                                <div class="modal-dialog">
                                    <div class="modal-content">
                                        <div class="modal-header">
                                            <h5 class="modal-title" id="deleteConfirmationModalLabel">Confirm Deletion</h5>
                                            <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                                        </div>
                                        <div class="modal-body">
                                            <p>Are you sure you want to delete this category?</p>
                                        </div>
                                        <div class="modal-footer">
                                            <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Cancel</button>

                                            <!-- This button will submit the form when clicked -->
                                            <button type="submit" class="btn btn-danger">Delete</button>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </form>
                    </div>
                </td>
            </tr>
        }
    </tbody>
</table>
