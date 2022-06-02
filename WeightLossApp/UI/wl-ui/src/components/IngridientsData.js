import React,{ Component } from 'react';
import { constants } from '../Constants';
import BootstrapTable from 'react-bootstrap-table-next';
import filterFactory, { textFilter, numberFilter, } from 'react-bootstrap-table2-filter';
import paginationFactory from 'react-bootstrap-table2-paginator';

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
            prevState:null,
        }
    }

    addClick() {
        this.setState({
            modalTitle:"Adding new Ingridient",
            itemID:0,
            itemName:"asd",
            itemCalories:0,
            itemProteins:0,
            itemCarbohydrates:0,
            itemFats:0,
            prevState:this.state,
        });
    }

    editClick() {
         if (this.state.itemID === 0) {
             alert("Select item first!");
         }
    }


    createClick() {
        fetch(constants.API_URL + 'IngridientData', {
            method:'POST',
            headers:{
                'Content-Type':'application/json'
            },
            body: JSON.stringify({
                Name:this.state.itemName,
                Calories:this.state.itemCalories,
                Proteins:this.state.itemProteins,
                Carbohydrates:this.state.itemCarbohydrates,
                Fats:this.state.itemFats,
            }),
        })
        .then(res=>res.json())
        .then((result)=>{
            this.refreshList();
        },(error)=>{
            alert('Failed');
        })
    }

    updateClick() {
        fetch(constants.API_URL + 'IngridientData', {
            method:'PUT',
            headers:{
                'Content-Type':'application/json'
            },
            body: JSON.stringify({
                Id:this.state.itemID,
                Name:this.state.itemName,
                Calories:this.state.itemCalories,
                Proteins:this.state.itemProteins,
                Carbohydrates:this.state.itemCarbohydrates,
                Fats:this.state.itemFats,
            }),
        })
        .then(res=>res.json())
        .then((result)=>{
            this.refreshList();
        },(error)=>{
            alert('Failed');
        })
    }

    deleteClick(id){
        if(window.confirm('Are you sure?')){
        fetch(constants.API_URL + 'IngridientData/' + id, {
            method:'DELETE',
            headers:{
                'Content-Type':'application/json'
            }
        })
        .then(res=>res.json())
        .then((result)=>{
            this.refreshList();
        },(error)=>{
            alert('Failed');
        })
        }
    }

    refreshList() {
        fetch(constants.API_URL + 'IngridientData')
        .then(response=>response.json())
        .then(data=> {
            this.setState({ingridientsData:data})
        });
    }

    componentDidMount() {
        this.refreshList();
    }

    changeName = (e) => {
        this.setState({itemName:e.target.value});
    }

    changeCalories = (e) => {
        this.setState({itemCalories:e.target.value});
    }

    changeFats = (e) => {
        this.setState({itemFats:e.target.value});
    }

    changeCarbohydrates = (e) => {
        this.setState({itemCarbohydrates:e.target.value});
    }

    changeProteins = (e) => {
        this.setState({itemProteins:e.target.value});
    }


    render() {

        const selectRow = {
            mode: 'radio',
            clickToSelect: true,
            style: { backgroundColor: '#f6f6f6' },
            onSelect: (row, isSelect, rowIndex, e) => {
                this.setState({
                    modalTitle:"Editing Ingridient",
                    itemID:row.Id,
                    itemName:row.Name,
                    itemCalories:row.Calories,
                    itemProteins:row.Proteins,
                    itemCarbohydrates:row.Carbohydrates,
                    itemFats:row.Fats,
                    prevState:this.state,
                });
            },
          };

        const columns = [{
            dataField: 'Id',
            sort: true,
            text: 'Ingridient ID',
            headerAlign: 'left',
          }, {
            dataField: 'Name',
            text: 'Name',
            sort: true,
            filter: textFilter(),
            headerAlign: 'left',
          }, {
            dataField: 'Calories',
            text: 'Calories',
            sort: true,
            filter: numberFilter(),
            headerAlign: 'left',
          }, {
            dataField: 'Proteins',
            text: 'Proteins',
            sort: true,
            filter: numberFilter(),
            headerAlign: 'left',
          },  {
            dataField: 'Carbohydrates',
            text: 'Carbohydrates',
            sort: true,
            filter: numberFilter(),
            headerAlign: 'left',
          }, {
            dataField: 'Fats',
            text: 'Fats',
            sort: true,
            filter: numberFilter(),
            headerAlign: 'left',
          },];

        return (
            <div style={{width:80 + 'vw'}}>
                <h3 className='m-5'>This is Ingridients page</h3>

                <BootstrapTable keyField='Id' data={ this.state.ingridientsData } columns={ columns } filter={ filterFactory() } filterPosition="top" pagination={ paginationFactory() } selectRow={ selectRow }/>


                <button type='button' className='btn btn-dark m-2 float-end' data-bs-toggle='modal'
                data-bs-target='#exampleModal' onClick={() => this.addClick()}>Add ingridient data</button>

                <button type='button' className='btn btn-dark m-2 float-end' data-bs-toggle='modal'
                data-bs-target='#exampleModal' onClick={() => this.editClick()} disabled={ this.state.itemID === 0 }>Edit ingridient data</button>

                <button type='button' className='btn btn-dark m-2 float-end' onClick={() => this.deleteClick(this.state.itemID)} disabled={ this.state.itemID === 0 }>Delete ingridient data</button>

                <div className="modal fade" id="exampleModal" tabIndex="-1" aria-hidden="true">
                <div className="modal-dialog modal-lg modal-dialog-centered">
                <div className="modal-content">
                <div className="modal-header">
                    <h5 className="modal-title">{this.state.modalTitle}</h5>
                    <button type="button" className="btn-close" data-bs-dismiss="modal" aria-label="Close"
                    ></button>
                </div>

                <div className="modal-body">
                    <div className="input-group mb-3">
                        <span className="input-group-text">Ingridient Name</span>
                        <input type="text" className="form-control"
                        value={this.state.itemName}
                        onChange={this.changeName}
                        />
                    </div>
                    <div className="input-group mb-3">
                        <span className="input-group-text">Ingridient Calories</span>
                        <input type="text" className="form-control"
                        value={this.state.itemCalories}
                        onChange={this.changeCalories}
                        />
                    </div>
                    <div className="input-group mb-3">
                        <span className="input-group-text">Ingridient Carbohydrates</span>
                        <input type="text" className="form-control"
                        value={this.state.itemCarbohydrates}
                        onChange={this.changeCarbohydrates}
                        />
                    </div>
                    <div className="input-group mb-3">
                        <span className="input-group-text">Ingridient Fats</span>
                        <input type="text" className="form-control"
                        value={this.state.itemFats}
                        onChange={this.changeFats}
                        />
                    </div>
                    <div className="input-group mb-3">
                        <span className="input-group-text">Ingridient Proteins</span>
                        <input type="text" className="form-control"
                        value={this.state.itemProteins}
                        onChange={this.changeProteins}
                        />
                    </div>

                        {this.state.itemID === 0?
                        <button type="button"
                        className="btn btn-primary float-start"
                        onClick={()=>this.createClick()}
                        >Create</button>
                        :null}

                        {this.state.itemID !== 0?
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

  