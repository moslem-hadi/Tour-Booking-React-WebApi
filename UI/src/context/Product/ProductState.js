import React, { useReducer } from 'react';
import ProductContext from './ProductContext';
import ProductReducer from './ProductReducer';

import {
    GET_LATEST_PRODUCTS
} from '../Types';

const ProductState = (props) => {
    const initialState = {
        products: []
    };

    const [state, dispatch] = useReducer(ProductReducer, initialState)

    //GET_LATEST_PRODUCTS


    return <ProductContext.Provider
        value={{
            //قابل دسترسی در همه پروژه
            Products: []
        }}
    >
        {props.children}
    </ProductContext.Provider>
}
export default ProductState;