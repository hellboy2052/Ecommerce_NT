import React,{useState,useEffect} from 'react';
import { Button, Form, FormGroup, Label, Input, FormText } from 'reactstrap';
import { post_images } from "../../actions/images";
import { useSelector, useDispatch } from "react-redux";
import { get_category_list } from "../../actions/category";
import { create_product, get_product_list, update_product } from "../../actions/product";
import { Link } from 'react-router-dom';
const UpdateProduct = ({match}) => {
    useEffect(() => {
        dispatch(get_category_list());
        dispatch(get_product_list())
        //checkVar();
      },[]);
      const {id}=match.params;
      console.log(id);
      const [imageFile, setImageFile] = useState([]);
      const [fileName, setFileName] = useState([]);
      const [product, setProduct] = useState({id:id});
      const dispatch = useDispatch();

      const { categoryList } = useSelector((state) => state.category);
      const { productList } = useSelector((state) => state.product);
      // const saveFile = (e) => {
      //   let arr1 = [];
      //   let arr2 = [];
      //   for (var i = 0; i < e.target.files.length; i++) {
      //     arr1.push(e.target.files[i]);
      //     arr2.push(e.target.files[i].name);
      //     // await setImageFile(imageFile.push(e.target.files[i]));
      //     // await setFileName(fileName.push(e.target.files[i].name))
      //   }
      //   setImageFile(...imageFile, arr1);
      //   setFileName(...fileName, arr2);

      // };
    
      // const postImage = async (e) => {
      //   let count=productList.data.length+1;
      //   console.log(productList)
      //   let formData = new FormData();
      //   for (var i = 0; i < fileName.length; i++) {
      //     formData.append("ImageFile", imageFile[i]);
      //     formData.append("FileName", fileName[i]);
      //     formData.append("ProductId", count);
      //     //axios.post("https://localhost:44309/api/Image",formData)
      //     await dispatch(post_images(formData));
      //   }
      //   console.log(fileName)
      // };
    
      const putProduct = async() => {
        dispatch(update_product(product))
        console.log(product)
      };
      var list_category = categoryList.data;
      const checkVar=()=>{
        var countPD = productList.data.length;
        console.log(countPD+ "check");
      }
      const btnClick=()=>{
        return(
           putProduct()
            //postImage()
        );
    }
    
  return (
    <div>
        <Form>
                <FormGroup>
                  <Label for="exampleEmail">Name</Label>
                  <Input
                    type="text"
                    name="name"
                    id="exampleEmail"
                    placeholder="Name"
                    onChange={(e)=>setProduct({...product,name:e.target.value})}
                  />
                </FormGroup>
                <FormGroup>
                  <Label for="examplePassword">Description</Label>
                  <Input
                    type="text"
                    name="description"
                    id="examplePassword"
                    placeholder="Description"
                    onChange={(e)=>setProduct({...product,description:e.target.value})}
                  />
                </FormGroup>
                <FormGroup>
                  <Label for="price">Price</Label>
                  <Input
                    type="text"
                    name="price"
                    id="price"
                    placeholder="Description"
                    onChange={(e)=>setProduct({...product,price:e.target.value})}
                  />
                </FormGroup>
                <FormGroup>
                  <Label for="exampleSelect">Select</Label>
                  <Input type="select" name="select" id="exampleSelect" defaultValue="1" onChange={(e)=>setProduct({...product,categoryId:e.target.value})}>
                    {list_category &&
                      list_category.map((e, i) => {
                        return <option key={i} value={i+1}>{e.name}</option>;
                      })}
                  </Input>
                </FormGroup>

                {/* <FormGroup>
                  <Label for="exampleFile">File</Label>
                  <Input
                    type="file"
                    multiple
                    name="file"
                    id="images"
                    onChange={saveFile}
                  />
                  <FormText color="muted">
                    This is some placeholder block-level help text for the above
                    input. It's a bit lighter and easily wraps to a new line.
                  </FormText>
                </FormGroup> */}


              </Form>
             {/* <Link to='/product'> </Link> */}
             <Link to='/category'><Button color="success" onClick={()=>{btnClick()}}>Update</Button></Link>
    </div>
  );
}

export default UpdateProduct;