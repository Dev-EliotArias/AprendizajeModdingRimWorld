using RimWorld;
using Verse;

namespace MesaAlquimia
{
    public class Building_MesaAlquimia : Building_WorkTable
    {
        private int ticksProcessing = 0;
        private bool isProcessing = false;
        private int silverStored = 0;
        private const int TicksToProcess = 12500;

        protected override void Tick()
        {
            base.Tick();

            // Accedemos al Worker directamente desde def.AllRecipes — funciona aunque
            // el bill ya haya sido eliminado del BillStack tras completarse.
            foreach (RecipeDef recipe in def.AllRecipes)
            {
                if (recipe.Worker is RecipeWorker_DepositarPlata worker && worker.HasPendingSilver)
                {
                    silverStored += worker.TakePendingSilver();
                    if (!isProcessing)
                    {
                        isProcessing = true;
                        ticksProcessing = 0;
                    }
                }
            }

            if (!isProcessing)
                return;

            if (!this.TryGetComp<CompPowerTrader>().PowerOn)
                return;

            ticksProcessing++;
            if (ticksProcessing >= TicksToProcess)
                FinishProcessing();
        }

        private void FinishProcessing()
        {
            isProcessing = false;
            ticksProcessing = 0;
            silverStored = 0;

            Thing monedas = ThingMaker.MakeThing(ThingDef.Named("Aprendizaje_MonedaOro"));
            monedas.stackCount = 5;
            GenPlace.TryPlaceThing(monedas, this.Position, this.Map, ThingPlaceMode.Near);

            Messages.Message("La mesa de alquimia ha creado monedas de oro.", this, MessageTypeDefOf.PositiveEvent);
        }

        public override void ExposeData()
        {
            base.ExposeData();
            Scribe_Values.Look(ref ticksProcessing, "ticksProcessing", 0);
            Scribe_Values.Look(ref isProcessing, "isProcessing", false);
            Scribe_Values.Look(ref silverStored, "silverStored", 0);
        }
    }
}
