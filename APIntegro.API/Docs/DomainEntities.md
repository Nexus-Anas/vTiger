# Domain Entities

## Project

```json
{
    "projectname": "Project Name",
    "startdate": "YYYY-MM-DD",
    "targetenddate": "YYYY-MM-DD",
    "actualenddate": "YYYY-MM-DD",
    "projectstatus": "Status",
    "projecttype": "Type",
    "linktoaccountscontacts": "Link (optional)",
    "assigned_user_id": "User ID",
    "project_no": "Project Number (optional)",
    "targetbudget": "Budget",
    "projecturl": "Project URL (optional)",
    "projectpriority": "Priority",
    "progress": "Progress",
    "createdtime": "YYYY-MM-DDTHH:MM:SS",
    "modifiedtime": "YYYY-MM-DDTHH:MM:SS",
    "modifiedby": "Modified By (optional)",
    "description": "Description (optional)",
    "source": "Source (optional)",
    "starred": "Starred (optional)",
    "tags": "Tags (optional)",
    "isconvertedfrompotential": "Converted From Potential (optional)",
    "potentialid": "Potential ID (optional)",
    "id": "ID (optional)",
    "label": "Label (optional)"
}
```
>Note: Ensure that the project name is unique and descriptive for easy identification.

## Project Tasks

```csharp
int a = 5;
```

```json
{
    "projecttaskname": "Task Name",
    "projecttasktype": "Task Type",
    "projecttaskpriority": "Task Priority",
    "projectid": "Project ID",
    "assigned_user_id": "User ID",
    "projecttasknumber": "Task Number (optional)",
    "projecttask_no": "Task Number (optional)",
    "projecttaskprogress": "Progress",
    "projecttaskhours": "Task Hours (optional)",
    "startdate": "YYYY-MM-DD",
    "enddate": "YYYY-MM-DD",
    "createdtime": "YYYY-MM-DDTHH:MM:SS",
    "modifiedtime": "YYYY-MM-DDTHH:MM:SS",
    "modifiedby": "Modified By (optional)",
    "description": "Description (optional)",
    "source": "Source (optional)",
    "starred": "Starred (optional)",
    "tags": "Tags (optional)",
    "projecttaskstatus": "Task Status",
    "id": "ID (optional)",
    "label": "Label (optional)"
}
```
>Note: Assign appropriate task priorities to ensure efficient task management.

## Contact

```json
{
    "salutationtype": "Salutation (optional)",
    "firstname": "First Name",
    "contactno": "Contact Number (optional)",
    "phone": "Phone Number",
    "lastname": "Last Name",
    "mobile": "Mobile Number (optional)",
    "accountid": "Account ID (optional)",
    "homephone": "Home Phone (optional)",
    "leadsource": "Lead Source (optional)",
    "otherphone": "Other Phone (optional)",
    "title": "Title (optional)",
    "fax": "Fax (optional)",
    "department": "Department",
    "birthday": "YYYY-MM-DD",
    "email": "Email Address",
    "contactid": "Contact ID (optional)",
    "assistant": "Assistant (optional)",
    "secondaryemail": "Secondary Email (optional)",
    "assistantphone": "Assistant Phone (optional)",
    "dontcall": "Do Not Call (optional)",
    "emailoptout": "Email Opt-Out (optional)",
    "assigned_user_id": "User ID",
    "reference": "Reference (optional)",
    "notifyowner": "Notify Owner (optional)",
    "createdtime": "YYYY-MM-DDTHH:MM:SS",
    "modifiedtime": "YYYY-MM-DDTHH:MM:SS",
    "modifiedby": "Modified By (optional)",
    "portal": "Portal (optional)",
    "supportstartdate": "Support Start Date (optional)",
    "supportenddate": "Support End Date (optional)",
    "mailingstreet": "Mailing Street (optional)",
    "otherstreet": "Other Street (optional)",
    "mailingcity": "Mailing City (optional)",
    "othercity": "Other City (optional)",
    "mailingstate": "Mailing State (optional)",
    "otherstate": "Other State (optional)",
    "mailingzip": "Mailing Zip (optional)",
    "otherzip": "Other Zip (optional)",
    "mailingcountry": "Mailing Country (optional)",
    "othercountry": "Other Country (optional)",
    "mailingpobox": "Mailing PO Box (optional)",
    "otherpobox": "Other PO Box (optional)",
    "imagename": "Image Name (optional)",
    "description": "Description (optional)",
    "isconvertedfromlead": "Converted From Lead (optional)",
    "source": "Source (optional)",
    "starred": "Starred (optional)",
    "tags": "Tags (optional)",
    "id": "ID (optional)",
    "label": "Label (optional)"
}
```
>Note: Verify email addresses to maintain communication accuracy.

## Organization

```json
{
    "accountname": "Organization Name",
    "account_no": "Account Number (optional)",
    "phone": "Phone Number",
    "website": "Website (optional)",
    "fax": "Fax",
    "tickersymbol": "Ticker Symbol (optional)",
    "otherphone": "Other Phone (optional)",
    "account_id": "Account ID (optional)",
    "email1": "Email Address 1",
    "employees": "Number of Employees",
    "email2": "Email Address 2 (optional)",
    "ownership": "Ownership (optional)",
    "rating": "Rating (optional)",
    "industry": "Industry (optional)",
    "siccode": "SIC Code (optional)",
    "accounttype": "Account Type (optional)",
    "annual_revenue": "Annual Revenue",
    "emailoptout": "Email Opt-Out (optional)",
    "notify_owner": "Notify Owner (optional)",
    "assigned_user_id": "User ID",
    "createdtime": "YYYY-MM-DDTHH:MM:SS",
    "modifiedtime": "YYYY-MM-DDTHH:MM:SS",
    "modifiedby": "Modified By (optional)",
    "bill_street": "Billing Street (optional)",
    "ship_street": "Shipping Street (optional)",
    "bill_city": "Billing City (optional)",
    "ship_city": "Shipping City (optional)",
    "bill_state": "Billing State (optional)",
    "ship_state": "Shipping State (optional)",
    "bill_code": "Billing Code (optional)",
    "ship_code": "Shipping Code (optional)",
    "bill_country": "Billing Country (optional)",
    "ship_country": "Shipping Country (optional)",
    "bill_pobox": "Billing PO Box (optional)",
    "ship_pobox": "Shipping PO Box (optional)",
    "description": "Description (optional)",
    "isconvertedfromlead": "Converted From Lead (optional)",
    "source": "Source (optional)",
    "starred": "Starred (optional)",
    "tags": "Tags (optional)",
    "id": "ID (optional)"
}
```
>Note: Keep track of annual revenue for financial planning and reporting.

## Project Milestone

```json
{
    "projectmilestonename": "Milestone Name",
    "projectmilestonedate": "YYYY-MM-DD",
    "projectid": "Project ID",
    "projectmilestonetype": "Milestone Type (optional)",
    "assigned_user_id": "User ID",
    "projectmilestone_no": "Milestone Number (optional)",
    "createdtime": "YYYY-MM-DDTHH:MM:SS",
    "modifiedtime": "YYYY-MM-DDTHH:MM:SS",
    "modifiedby": "Modified By (optional)",
    "description": "Description (optional)",
    "source": "Source (optional)",
    "starred": "Starred (optional)",
    "tags": "Tags (optional)",
    "id": "ID (optional)"
}
```
>Note: Set clear milestones to track project progress effectively.
