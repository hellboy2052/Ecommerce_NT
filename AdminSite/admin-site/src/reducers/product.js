import * as product from "../contains/product";

const initialState = {
    productList: [],
    product_selected: {}
};

export default (state = initialState, { type, payload }) => {
    switch (type) {
        case product.PRODUCT_LIST: {

            state.productList = payload;
            return { ...state };

        }
        case product.CREATE_PRODUCT:{
            console.log(payload);
            return { ...state };
        }       
        default:
            return state;
    }
}