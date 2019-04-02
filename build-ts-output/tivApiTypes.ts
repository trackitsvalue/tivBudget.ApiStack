// ..\src\tivBudget.Dal\Models\Account.cs
export interface Account {
    id: string;
    accountTemplateId: string;
    accountTypeId: number;
    description: string;
    ownerId: string;
    areAccountCategoriesOpen: boolean;
    displayIndex: number;
    createdOn: string;
    createdBy: string;
    modifiedOn?: string;
    modifiedBy: string;
    ts: string;
    accountTemplate: AccountTemplate;
    accountType: AccountType;
    owner: User;
    accountActualRecurrences: AccountActualRecurrence[];
    accountCategories: AccountCategory[];
    budgetItems: BudgetItem[];
    isNew: boolean;
    isDirty: boolean;
}

// ..\src\tivBudget.Dal\Models\AccountActual.cs
export interface AccountActual {
    id: string;
    actualTemplateId: string;
    budgetActualLinkId?: string;
    categoryId: string;
    description: string;
    relevantOn: string;
    amount: number;
    isLinked: boolean;
    isRecurring: boolean;
    createdOn: string;
    createdBy: string;
    modifiedOn?: string;
    modifiedBy: string;
    ts: string;
    actualTemplate: AccountActualTemplate;
    budgetActualLink: BudgetActual;
    category: AccountCategory;
    isNew: boolean;
    isDirty: boolean;
}

// ..\src\tivBudget.Dal\Models\AccountActualRecurrence.cs
export interface AccountActualRecurrence {
    id: string;
    actualTemplateId: string;
    accountId: string;
    description: string;
    relevantDay?: number;
    amount?: number;
    percent?: number;
    createdOn: string;
    createdBy: string;
    modifiedOn?: string;
    modifiedBy: string;
    ts: string;
    account: Account;
    actualTemplate: AccountActualTemplate;
    isNew: boolean;
    isDirty: boolean;
}

// ..\src\tivBudget.Dal\Models\AccountActualTemplate.cs
export interface AccountActualTemplate {
    id: string;
    accountTemplateId: string;
    description: string;
    isDeposit: boolean;
    isDefault: boolean;
    allowRecurringAmount: boolean;
    allowRecurringPercent: boolean;
    allowRecurringDay?: boolean;
    ownerId?: string;
    createdOn: string;
    createdBy: string;
    modifiedOn?: string;
    modifiedBy: string;
    ts: string;
    accountTemplate: AccountTemplate;
    owner: User;
    accountActualRecurrences: AccountActualRecurrence[];
    accountActuals: AccountActual[];
    isNew: boolean;
    isDirty: boolean;
}

// ..\src\tivBudget.Dal\Models\AccountCategory.cs
export interface AccountCategory {
    id: string;
    categoryTemplateId: string;
    description: string;
    areAccountActualsOpen: boolean;
    accountId: string;
    displayIndex: number;
    isDefault: boolean;
    createdOn: string;
    createdBy: string;
    modifiedOn?: string;
    modifiedBy: string;
    ts: string;
    account: Account;
    categoryTemplate: AccountCategoryTemplate;
    accountActuals: AccountActual[];
    budgetItems: BudgetItem[];
    isNew: boolean;
    isDirty: boolean;
}

// ..\src\tivBudget.Dal\Models\AccountCategoryTemplate.cs
export interface AccountCategoryTemplate {
    id: string;
    accountTemplateId: string;
    description: string;
    isDefault: boolean;
    ownerId?: string;
    icon: string;
    backgroundColor: string;
    createdOn: string;
    createdBy: string;
    modifiedOn?: string;
    modifiedBy: string;
    ts: string;
    accountTemplate: AccountTemplate;
    owner: User;
    accountCategories: AccountCategory[];
    isNew: boolean;
    isDirty: boolean;
}

// ..\src\tivBudget.Dal\Models\AccountTemplate.cs
export interface AccountTemplate {
    id: string;
    typeId: number;
    description: string;
    ownerId?: string;
    icon: string;
    isIncomeAccount: boolean;
    createdOn: string;
    createdBy: string;
    modifiedOn?: string;
    modifiedBy: string;
    ts: string;
    owner: User;
    type: AccountType;
    accountActualTemplates: AccountActualTemplate[];
    accountCategoryTemplates: AccountCategoryTemplate[];
    accounts: Account[];
    budgetItemTemplatesLinkableAccountTemplate: BudgetItemTemplate[];
    budgetItemTemplatesTransferableAccountTemplate: BudgetItemTemplate[];
    isNew: boolean;
    isDirty: boolean;
}

// ..\src\tivBudget.Dal\Models\AccountType.cs
export interface AccountType {
    id: number;
    displayIndex: number;
    descriptionPlural: string;
    descriptionSingular: string;
    posLineItemShortDescription: string;
    posLineItemDescription: string;
    negLineItemShortDescription: string;
    negLineItemDescription: string;
    createdOn: string;
    createdBy: string;
    modifiedOn?: string;
    modifiedBy: string;
    ts: string;
    accountTemplates: AccountTemplate[];
    accounts: Account[];
    budgetItemTemplatesLinkableAccountType: BudgetItemTemplate[];
    budgetItemTemplatesTransferableAccountType: BudgetItemTemplate[];
    isNew: boolean;
    isDirty: boolean;
}

// ..\src\tivBudget.Dal\Models\Application.cs
export interface Application {
    id: number;
    name: string;
    createdOn: string;
    createdBy: string;
    modifiedOn?: string;
    modifiedBy: string;
    ts: string;
    userSettings: UserSetting[];
    isNew: boolean;
    isDirty: boolean;
}

// ..\src\tivBudget.Dal\Models\Budget.cs
export interface Budget {
    id: string;
    description: string;
    year: number;
    month: number;
    ownerId: string;
    displayIndex: number;
    actualIncome: number;
    estimatedIncome: number;
    actualMinusEstimatedIncome: number;
    actualSpending: number;
    estimatedSpending: number;
    actualRemaining: number;
    estimatedRemaining: number;
    createdOn: string;
    createdBy: string;
    modifiedOn?: string;
    modifiedBy: string;
    startDate: string;
    ts: string;
    budgetCategories: BudgetCategory[];
    isNew: boolean;
    isDirty: boolean;
}

// ..\src\tivBudget.Dal\Models\BudgetActual.cs
export interface BudgetActual {
    id: string;
    description: string;
    amount: number;
    relevantOn: string;
    itemId: string;
    isLinked: boolean;
    isEnvelopeDeposit: boolean;
    displayIndex: number;
    createdOn: string;
    createdBy: string;
    modifiedOn?: string;
    modifiedBy: string;
    ts: string;
    item: BudgetItem;
    accountActuals: AccountActual[];
    isNew: boolean;
    isDirty: boolean;
}

// ..\src\tivBudget.Dal\Models\BudgetCategory.cs
export interface BudgetCategory {
    id: string;
    categoryTemplateId: string;
    description: string;
    areBudgetItemsOpen: boolean;
    budgetId: string;
    displayIndex: number;
    categoryBudgeted: number;
    categorySpent: number;
    categoryRemaining: number;
    createdOn: string;
    createdBy: string;
    modifiedOn?: string;
    modifiedBy: string;
    ts: string;
    budget: Budget;
    categoryTemplate: BudgetCategoryTemplate;
    budgetItems: BudgetItem[];
    isNew: boolean;
    isDirty: boolean;
}

// ..\src\tivBudget.Dal\Models\BudgetCategoryTemplate.cs
export interface BudgetCategoryTemplate {
    id: string;
    description: string;
    ownerId?: string;
    icon: string;
    isIncomeCategory: boolean;
    backgroundColor: string;
    createdOn: string;
    createdBy: string;
    modifiedOn?: string;
    modifiedBy: string;
    ts: string;
    owner: User;
    budgetCategories: BudgetCategory[];
    budgetItemTemplates: BudgetItemTemplate[];
    isNew: boolean;
    isDirty: boolean;
}

// ..\src\tivBudget.Dal\Models\BudgetItem.cs
export interface BudgetItem {
    id: string;
    itemTemplateId: string;
    categoryId: string;
    description: string;
    amountBudgeted: number;
    areBudgetActualsOpen: boolean;
    isLinked: boolean;
    displayIndex: number;
    itemSpent: number;
    itemRemaining: number;
    accountLinkId?: string;
    accountCategoryLinkId?: string;
    recurringSettingsId?: string;
    alertId?: string;
    createdOn: string;
    createdBy: string;
    modifiedOn?: string;
    modifiedBy: string;
    ts: string;
    accountCategoryLink: AccountCategory;
    accountLink: Account;
    alert: BudgetItemAlert;
    category: BudgetCategory;
    itemTemplate: BudgetItemTemplate;
    recurringSettings: BudgetItemRecurringSetting;
    budgetActuals: BudgetActual[];
    isNew: boolean;
    isDirty: boolean;
}

// ..\src\tivBudget.Dal\Models\BudgetItemAlert.cs
export interface BudgetItemAlert {
    id: string;
    alertType: number;
    acknowledged: boolean;
    createdOn: string;
    createdBy: string;
    modifiedOn?: string;
    modifiedBy: string;
    ts: string;
    budgetItems: BudgetItem[];
    isNew: boolean;
    isDirty: boolean;
}

// ..\src\tivBudget.Dal\Models\BudgetItemRecurringSetting.cs
export interface BudgetItemRecurringSetting {
    id: string;
    dayDue?: number;
    friendlyReminder?: number;
    warningReminder?: number;
    sendViaEmail: boolean;
    sendViaText: boolean;
    createdOn: string;
    createdBy: string;
    modifiedOn?: string;
    modifiedBy: string;
    ts: string;
    budgetItems: BudgetItem[];
    isNew: boolean;
    isDirty: boolean;
}

// ..\src\tivBudget.Dal\Models\BudgetItemTemplate.cs
export interface BudgetItemTemplate {
    id: string;
    categoryTemplateId: string;
    description: string;
    ownerId?: string;
    isLinkable: boolean;
    linkableAccountTypeId?: number;
    linkableAccountTemplateId?: string;
    isTransferable: boolean;
    transferableAccountTypeId?: number;
    transferableAccountTemplateId?: string;
    isEnvelopeAllowed: boolean;
    createdOn: string;
    createdBy: string;
    modifiedOn?: string;
    modifiedBy: string;
    ts: string;
    categoryTemplate: BudgetCategoryTemplate;
    linkableAccountTemplate: AccountTemplate;
    linkableAccountType: AccountType;
    owner: User;
    transferableAccountTemplate: AccountTemplate;
    transferableAccountType: AccountType;
    budgetItems: BudgetItem[];
    isNew: boolean;
    isDirty: boolean;
}

// ..\src\tivBudget.Dal\Models\Group.cs
export interface Group {
    id: number;
    description: string;
    isNewUserDefault: boolean;
    createdOn: string;
    createdBy: string;
    modifiedOn?: string;
    modifiedBy: string;
    ts: string;
    isNew: boolean;
    isDirty: boolean;
}

// ..\src\tivBudget.Dal\Models\ReportCategory.cs
export interface ReportCategory {
    id: number;
    name: string;
    displayIndex: number;
    createdOn: string;
    createdBy: string;
    modifiedOn?: string;
    modifiedBy: string;
    ts: string;
    isNew: boolean;
    isDirty: boolean;
}

// ..\src\tivBudget.Dal\Models\User.cs
export interface User {
    id: string;
    userName: string;
    email: string;
    password: string;
    passwordSalt: string;
    passwordVersion: number;
    isLocked: boolean;
    isEnabled: boolean;
    reenableToken?: string;
    resetPasswordToken?: string;
    resetPasswordTokenCreatedOn?: string;
    lastLoginDate?: string;
    lastLoginToken?: string;
    lastPasswordChangeDate: string;
    lastLockoutDate?: string;
    failedPasswordAttemptCount: number;
    notes: string;
    groupAssociation: number;
    createdOn: string;
    createdBy: string;
    modifiedOn?: string;
    modifiedBy: string;
    ts: string;
    accountActualTemplates: AccountActualTemplate[];
    accountCategoryTemplates: AccountCategoryTemplate[];
    accountTemplates: AccountTemplate[];
    accounts: Account[];
    budgetCategoryTemplates: BudgetCategoryTemplate[];
    budgetItemTemplates: BudgetItemTemplate[];
    userSettings: UserSetting[];
    isNew: boolean;
    isDirty: boolean;
}

// ..\src\tivBudget.Dal\Models\UserSetting.cs
export interface UserSetting {
    userId: string;
    applicationId: number;
    name: string;
    value: string;
    isCacheable?: boolean;
    isWritable?: boolean;
    createdOn: string;
    createdBy: string;
    modifiedOn?: string;
    modifiedBy: string;
    ts: string;
    application: Application;
    user: User;
    isNew: boolean;
    isDirty: boolean;
}
