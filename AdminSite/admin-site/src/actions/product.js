import api from '../api'
import {CREATE_PRODUCT, PRODUCT_LIST} from "../contains/product";
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
export const create_product=(product)=>async(dispatch)=>{
    try{
        const data=await api.Product.createProduct(product);
        dispatch({
            type: CREATE_PRODUCT,
            payload:data
        })
       
    }catch(error){
        console.log(error);
    }
}