export class parentCat {
    categoryParentId: number;
    parentCategoryName: string;
    subCategory: subCat[];
}

export class subCat {
    categoryId: number;
    categoryName: string;
    docLists: docs[];
}

export class docs {
    documentId: number;
    domainId: number;
    clientId: number;
    documentName: string;
    projectId: number;
    userId: number;
    details: string;
    description: string;
}

export class addDoc {
    
    documentName: string;
    details: string;
    description: string;
}

export class categoryList {
    
    domainId: number;
    name: string;
    id: number;
}


export class AddNewDoc {
    
    domainId: number;
    clientId: number;
    documentName: string;
    categoryId: number;
    categoryParentId: number;
    categoryName: string;
    projectId: number;
    userId: number;
    details: string;
    description: string;

}
