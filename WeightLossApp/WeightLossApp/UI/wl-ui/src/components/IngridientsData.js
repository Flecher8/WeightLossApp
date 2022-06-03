import React,{Component} from 'react';
import { constants } from '../Constants';

export class IngridientsData extends Component {

    constructor(props) {
        super(props);

        this.state = {
            ingridientsData:[],
            modalTitle:"",
            itemID:0,
            itemName:"",
            itemCalories:0,
            itemProteins:0,
            itemCarbohydrates:0,
            itemFats:0,
        }
    }

    refreshList() {
        fetch(constants.API_URL + 'IngridientData')
        .then(response=>response.json())
        .then(data=> {
            this.setState({ingridientsData:data})
        });
    }

    addClick() {
        this.setState({
            modalTitle:"Adding new Ingridient",
            itemID:0,
            itemName:"",
            itemCalories:0,
            itemProteins:0,
            itemCarbohydrates:0,
            itemFats:0,
        });
    }

    componentDidMount() {
        this.refreshList();
    }


    render() {

        const {
            ingridientsData,
            modalTitle,
            itemID,
            itemName,
            itemCalories,
            itemProteins,
            itemCarbohydrates,
            itemFats,
        } = this.state;

        return (
            <div style={{width:80 + 'vw'}}>
                <h3 className='m-5'>This is Ingridients page</h3>
                <table className='table table-striped'>
                    <thead>
                        <tr>
                            <th>ID</th>
                            <th>Name</th>
                            <th>Calories</th>
                            <th>Proteins</th>
                            <th>Carbohydrates</th>
                            <th>Fats</th>
                            <th>Options</th>
                        </tr>
                    </thead>
                    <tbody>
                        {ingridientsData.map(item => 
                            <tr>
                                <td>{item.ID}</td>
                                <td>{item.Name}</td>
                                <td>{item.Calories}</td>
                                <td>{item.Proteins}</td>
                                <td>{item.Carbohydrates}</td>
                                <td>{item.Fats}</td>
                                <td>
                                    <button type='button' className='btn btn-dark mx-2'>Edit</button>
                                    <button type='button' className='btn btn-danger mx-2'>Delete</button>
                                </td>
                            </tr>
                        )}
                    </tbody>
                </table>

                <button type='button' className='btn btn-dark m-2 float-end' data-bs-toggle='modal'
                data-bs-target='#exampleModal' onClick={() => this.addClick()}>Add ingridient data</button>



                <div className="modal fade" id="exampleModal" tabIndex="-1" aria-hidden="true">
                <div className="modal-dialog modal-lg modal-dialog-centered">
                <div className="modal-content">
                <div className="modal-header">
                    <h5 className="modal-title">{modalTitle}</h5>
                    <button type="button" className="btn-close" data-bs-dismiss="modal" aria-label="Close"
                    ></button>
                </div>

                <div className="modal-body">
                    <div className="input-group mb-3">
                        <span className="input-group-text">Ingridient Name</span>
                        <input type="text" className="form-control"
                        value={itemName}
                        //onChange={this.changeDepartmentName}
                        />
                    </div>

                        {itemID === 0?
                        <button type="button"
                        className="btn btn-primary float-start"
                        onClick={()=>this.createClick()}
                        >Create</button>
                        :null}

                        {itemID !== 0?
                        <button type="button"
                        className="btn btn-primary float-start"
                        onClick={()=>this.updateClick()}
                        >Update</button>
                        :null}

                </div>
                </div>
                </div> 
                </div>



            </div>
                
        )
    }
}