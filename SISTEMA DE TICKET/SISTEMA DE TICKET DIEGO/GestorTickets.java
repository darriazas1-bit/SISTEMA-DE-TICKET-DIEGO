
import java.util.ArrayList;
import java.util.List;

public class GestorTickets {
    private List<Ticket> lstTickets;
    private List<Tecnico> lstTecnicos;
    private List<Solicitante> lstSolicitantes;
    private int intContador;

    public GestorTickets() {
        this.lstTickets = new ArrayList<>();
        this.lstTecnicos = new ArrayList<>();
        this.lstSolicitantes = new ArrayList<>();
        this.intContador = 1000;
    }

    public void registrarTecnico(Tecnico t) { lstTecnicos.add(t); }
    public void registrarSolicitante(Solicitante s) { lstSolicitantes.add(s); }

    public Ticket crearTicket(String titulo, String desc, String prioridad, Solicitante sol) {
        intContador++;
        Ticket t = new Ticket(intContador, titulo, desc, prioridad, sol);
        lstTickets.add(t);
        
        // Asignar técnico con capacidad
        for (Tecnico tec : lstTecnicos) {
            if (tec.tieneCapacidad()) {
                t.asignarTecnico(tec);
                break;
            }
        }
        return t;
    }

    public Ticket buscarTicket(int id) {
        for (Ticket t : lstTickets) {
            if (t.getIntIdTicket() == id) return t;
        }
        throw new IllegalArgumentException("KeyNotFoundException: Ticket no existe.");
    }

    public void verFlujoDeTicket(int id) {
        Ticket t = buscarTicket(id);
        System.out.println("=== FLUJO DEL TICKET #" + id + " ===");
        for (FlujoTicket f : t.getLstFlujoTicket()) {
            System.out.println(f.toString());
        }
    }
}