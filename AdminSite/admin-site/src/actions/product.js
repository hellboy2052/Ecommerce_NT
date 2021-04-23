import api from '../api'
import {PRODUCT_LIST} from "../contains/product";
export const get_product_list = () => async (dispatch) => {
    try {
        const data = await api.Product.getAllProducts();

        dispatch({
            type: PRODUCT_LIST,
            payload: data,
        });

    } catch (error) {
        console.log(error);
    }
};